using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Text;
using System.Net.Http;
using Amazon.S3;
using Amazon;
using Amazon.S3.Model;
using Amazon.S3.Transfer;

namespace btnproject
{
    public partial class Main : Form
    {

        //카메라 on off 확인 플래그
        bool isCameraOn;
        //카메라 스트리밍 루프 플래그
        bool stop;
        //비동기 처리 작업
        Task task;
        Mat mat;
        VideoCapture videoCapture;

        // TCP Client 핸들러
        private TcpClientHandler tcpClientHandler;

        private static readonly string bucketName = "your-bucket-name";
        private static readonly string accessKey = "your-access-key";
        private static readonly string secretKey = "your-secret-key";
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.USEast1; // 예: US East (N. Virginia)
        private static IAmazonS3 s3Client;

        private int trans_number = 1;
        public Main()
        {
            InitializeComponent();
            pb_cam.SizeMode = PictureBoxSizeMode.StretchImage;
            pb_histo.SizeMode = PictureBoxSizeMode.StretchImage;
            videoCapture = new VideoCapture(0, VideoCaptureAPIs.DSHOW);
            isCameraOn = false;
            stop = false;

            // TCP Client 초기화 (IP 주소와 포트 번호를 라즈베리파이 서버에 맞게 설정)

            s3Client = new AmazonS3Client(accessKey, secretKey, bucketRegion);


        }

        //START 클릭
        private async void btn_start_Click(object sender, EventArgs e)
        {
            //카메라 꺼져있을시.
            if (!isCameraOn)
            {
                tcpClientHandler.SendMessageAsync("start");
                this.Invoke((Action)(() =>
                {
                    dgv_trans.Rows.Add(trans_number++, "송신", "컨베이어 작동", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                }));
                stop = false;

                //이전에 카메라 작업이 존재한다면
                if (task != null)
                {
                    //CameraCallback 메서드의 while 루프를 종료하게 만듭니다.
                    stop = true;
                    //현재 실행중인 카메라 작업이 종료될때까지 대기
                    await task;
                    //작업이 끝난 후에 Task 객체의 리소스를 해제합니다.
                    task.Dispose();
                    //cameraTask 참조를 null로 설정하여 작업이 종료되었음을 명확히 합니다.
                    task = null;
                }

                //새로운 카메라 스트림 열기
                videoCapture = new VideoCapture(0, VideoCaptureAPIs.DSHOW);
                //CameraCallback 메서드를 비동기적으로 실행하여 카메라 스트리밍을 처리합니다
                task = Task.Run(() => CameraCallback());
                isCameraOn = true;
            }
        }

        //STOP 클릭
        private async void btn_stop_Click(object sender, EventArgs e)
        {
            stop = true;

            //이전에 카메라 작업이 존재한다면
            if (task != null)
            {
                await task;
                task.Dispose();
                task = null;
            }

            isCameraOn = false;

            if (videoCapture != null && videoCapture.IsOpened())
            {
                videoCapture.Release();
                videoCapture.Dispose();
                videoCapture = null;
            }

            // TCP 연결 종료
            tcpClientHandler.SendMessageAsync("Camera Stopped");
            tcpClientHandler.Disconnect();
        }

        private async Task CameraCallback()
        {
            mat = new Mat();

            while (!stop)
            {
                if (videoCapture != null && videoCapture.IsOpened())
                {
                    videoCapture.Read(mat);
                    if (!mat.Empty())
                    {
                        var bitmap = BitmapConverter.ToBitmap(mat);

                        pb_cam.Invoke((Action)(() =>
                        {
                            pb_cam.Image = bitmap;
                        }));
                    }
                }
                await Task.Delay(30);  // 프레임 속도 조절
            }

            mat.Release();
        }

        private void AnalyzeColor(Mat image)
        {
            //컨베이어 벨트 중지시
            if (isCameraOn && !mat.Empty())
            {
                Mat StopImage = mat.Clone();

                Scalar mean = Cv2.Mean(StopImage);

                double blueMean = mean.Val0;
                double greenMean = mean.Val1;
                double redMean = mean.Val2;

                string dominantColor;

                //히스토그램 rgb 구분 경계값 지정
                double threshold = 17;

                if (redMean > greenMean + threshold && redMean > blueMean + threshold)
                {
                    dominantColor = "Red";
                    pb_histo.BackColor = Color.Red;
                }
                else if (greenMean > redMean + threshold && greenMean > blueMean + threshold)
                {
                    dominantColor = "Green";
                }
                else if (blueMean > redMean + threshold && blueMean > greenMean + threshold)
                {
                    dominantColor = "Blue";
                }
                else
                {
                    dominantColor = "Nothing"; // Default case when no color is dominant
                }
            }
        }

        // 이미지 업로드 
        private async void UploadImageToS3(string filePath)
        {
            try
            {
                var fileTransferUtility = new TransferUtility(s3Client);

                // 파일 업로드
                await fileTransferUtility.UploadAsync(filePath, bucketName);
                MessageBox.Show("Image uploaded successfully!");
            }
            catch (AmazonS3Exception e)
            {
                MessageBox.Show($"Error encountered on server. Message:'{e.Message}'");
            }
            catch (Exception e)
            {
                MessageBox.Show($"Unknown error encountered. Message:'{e.Message}'");
            }
        }

        private void btn_modify_Click(object sender, EventArgs e)
        {
            //UploadToS3("BackStorage", "/2024-08-13", "C:\\SmartFactory");
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    UploadImageToS3(filePath);
                }
            }
        }

        //이미지 다운로드
        private async void DownloadImageFromS3(string keyName, string destinationPath)
        {
            try
            {
                var request = new GetObjectRequest
                {
                    BucketName = bucketName,
                    Key = keyName
                };

                using (GetObjectResponse response = await s3Client.GetObjectAsync(request))
                using (Stream responseStream = response.ResponseStream)
                using (FileStream fileStream = new FileStream(destinationPath, FileMode.Create, FileAccess.Write))
                {
                    byte[] buffer = new byte[8192];
                    int bytesRead;
                    while ((bytesRead = responseStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        fileStream.Write(buffer, 0, bytesRead);
                    }
                    MessageBox.Show("Image downloaded successfully!");
                }
            }
            catch (AmazonS3Exception e)
            {
                MessageBox.Show($"Error encountered on server. Message:'{e.Message}'");
            }
            catch (Exception e)
            {
                MessageBox.Show($"Unknown error encountered. Message:'{e.Message}'");
            }
        }


        private void btn_load_Click(object sender, EventArgs e)
        {
            string keyName = "your-image-key"; // S3에서 이미지의 키
            string destinationPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "downloaded_image.jpg");
            DownloadImageFromS3(keyName, destinationPath);
        }

        private async void btn_connect_Click(object sender, EventArgs e)
        {
            string serverip;
            int serverport;
            if (tb_server.Text == "" || tb_port.Text == "")
            {
                MessageBox.Show("서버IP, Port를 입력해주세요");
            }
            else
            {
                serverip = tb_server.Text;
                serverport = int.Parse(tb_port.Text);
                tcpClientHandler = new TcpClientHandler(serverip, serverport);

                // TCP 서버에 연결
                try
                {
                    await tcpClientHandler.ConnectAsync();
                    //tcpClientHandler.SendMessageAsync("start");

                    tcpClientHandler.MessageReceived += TcpClientHandler_MessageReceived; // 이벤트 핸들러 등록

                    pb_hw.BackColor = Color.Green;
                    pb_sv.BackColor = Color.Green;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    pb_hw.BackColor = Color.Red;
                    pb_sv.BackColor = Color.Red;
                    return;
                }
            }
        }

        private void btn_logdel_Click(object sender, EventArgs e)
        {
            dgv_trans.Rows.Clear();
        }

        private void TcpClientHandler_MessageReceived(string message)
        {

            // 수신한 메시지를 DataGridView에 출력
            this.Invoke((Action)(() =>
            {
                dgv_trans.Rows.Add(trans_number++, "수신", message, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            }));
        }


    }
}


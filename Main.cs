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

        //ī�޶� on off Ȯ�� �÷���
        bool isCameraOn;
        //ī�޶� ��Ʈ���� ���� �÷���
        bool stop;
        //�񵿱� ó�� �۾�
        Task task;
        Mat mat;
        VideoCapture videoCapture;

        // TCP Client �ڵ鷯
        private TcpClientHandler tcpClientHandler;

        private static readonly string bucketName = "your-bucket-name";
        private static readonly string accessKey = "your-access-key";
        private static readonly string secretKey = "your-secret-key";
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.USEast1; // ��: US East (N. Virginia)
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

            // TCP Client �ʱ�ȭ (IP �ּҿ� ��Ʈ ��ȣ�� ��������� ������ �°� ����)

            s3Client = new AmazonS3Client(accessKey, secretKey, bucketRegion);


        }

        //START Ŭ��
        private async void btn_start_Click(object sender, EventArgs e)
        {
            //ī�޶� ����������.
            if (!isCameraOn)
            {
                tcpClientHandler.SendMessageAsync("start");
                this.Invoke((Action)(() =>
                {
                    dgv_trans.Rows.Add(trans_number++, "�۽�", "�����̾� �۵�", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                }));
                stop = false;

                //������ ī�޶� �۾��� �����Ѵٸ�
                if (task != null)
                {
                    //CameraCallback �޼����� while ������ �����ϰ� ����ϴ�.
                    stop = true;
                    //���� �������� ī�޶� �۾��� ����ɶ����� ���
                    await task;
                    //�۾��� ���� �Ŀ� Task ��ü�� ���ҽ��� �����մϴ�.
                    task.Dispose();
                    //cameraTask ������ null�� �����Ͽ� �۾��� ����Ǿ����� ��Ȯ�� �մϴ�.
                    task = null;
                }

                //���ο� ī�޶� ��Ʈ�� ����
                videoCapture = new VideoCapture(0, VideoCaptureAPIs.DSHOW);
                //CameraCallback �޼��带 �񵿱������� �����Ͽ� ī�޶� ��Ʈ������ ó���մϴ�
                task = Task.Run(() => CameraCallback());
                isCameraOn = true;
            }
        }

        //STOP Ŭ��
        private async void btn_stop_Click(object sender, EventArgs e)
        {
            stop = true;

            //������ ī�޶� �۾��� �����Ѵٸ�
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

            // TCP ���� ����
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
                await Task.Delay(30);  // ������ �ӵ� ����
            }

            mat.Release();
        }

        private void AnalyzeColor(Mat image)
        {
            //�����̾� ��Ʈ ������
            if (isCameraOn && !mat.Empty())
            {
                Mat StopImage = mat.Clone();

                Scalar mean = Cv2.Mean(StopImage);

                double blueMean = mean.Val0;
                double greenMean = mean.Val1;
                double redMean = mean.Val2;

                string dominantColor;

                //������׷� rgb ���� ��谪 ����
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

        // �̹��� ���ε� 
        private async void UploadImageToS3(string filePath)
        {
            try
            {
                var fileTransferUtility = new TransferUtility(s3Client);

                // ���� ���ε�
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

        //�̹��� �ٿ�ε�
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
            string keyName = "your-image-key"; // S3���� �̹����� Ű
            string destinationPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "downloaded_image.jpg");
            DownloadImageFromS3(keyName, destinationPath);
        }

        private async void btn_connect_Click(object sender, EventArgs e)
        {
            string serverip;
            int serverport;
            if (tb_server.Text == "" || tb_port.Text == "")
            {
                MessageBox.Show("����IP, Port�� �Է����ּ���");
            }
            else
            {
                serverip = tb_server.Text;
                serverport = int.Parse(tb_port.Text);
                tcpClientHandler = new TcpClientHandler(serverip, serverport);

                // TCP ������ ����
                try
                {
                    await tcpClientHandler.ConnectAsync();
                    //tcpClientHandler.SendMessageAsync("start");

                    tcpClientHandler.MessageReceived += TcpClientHandler_MessageReceived; // �̺�Ʈ �ڵ鷯 ���

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

            // ������ �޽����� DataGridView�� ���
            this.Invoke((Action)(() =>
            {
                dgv_trans.Rows.Add(trans_number++, "����", message, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            }));
        }


    }
}


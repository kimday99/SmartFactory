using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Text;
using System.Net.Http;


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


        public Main()
        {
            InitializeComponent();
            pb_cam.SizeMode = PictureBoxSizeMode.StretchImage;
            pb_histo.SizeMode = PictureBoxSizeMode.StretchImage;
            videoCapture = new VideoCapture(0, VideoCaptureAPIs.DSHOW);
            isCameraOn = false;
            stop = false;

            // TCP Client 초기화 (IP 주소와 포트 번호를 라즈베리파이 서버에 맞게 설정)
            tcpClientHandler = new TcpClientHandler("172.30.1.43", 65432);
        }


        //START 클릭
        private async void btn_start_Click(object sender, EventArgs e)
        {
            //카메라 꺼져있을시.
            if (!isCameraOn)
            {
                // TCP 서버에 연결
                try
                {
                    await tcpClientHandler.ConnectAsync();
                    tcpClientHandler.SendMessageAsync("start");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }

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

    }
}

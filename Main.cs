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


        public Main()
        {
            InitializeComponent();
            pb_cam.SizeMode = PictureBoxSizeMode.StretchImage;
            pb_histo.SizeMode = PictureBoxSizeMode.StretchImage;
            videoCapture = new VideoCapture(0, VideoCaptureAPIs.DSHOW);
            isCameraOn = false;
            stop = false;

            // TCP Client �ʱ�ȭ (IP �ּҿ� ��Ʈ ��ȣ�� ��������� ������ �°� ����)
            tcpClientHandler = new TcpClientHandler("172.30.1.43", 65432);
        }


        //START Ŭ��
        private async void btn_start_Click(object sender, EventArgs e)
        {
            //ī�޶� ����������.
            if (!isCameraOn)
            {
                // TCP ������ ����
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

    }
}

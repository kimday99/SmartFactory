using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Text;
using System.Net.Http;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Net.Mail;


namespace btnproject
{
    public partial class Main : Form
    {
        // DatabaseHandler ��ü
        private DatabaseHandler dbHandler;
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
        // AWS Ŭ���̾�Ʈ �߰�
        private AwsClient awsClient;
        //ID 
        private int photoSequenceNumber = 1;

        private HttpClient httpClient;


        public Main()
        {
            InitializeComponent();
            pb_cam.SizeMode = PictureBoxSizeMode.StretchImage;
            pb_histo.SizeMode = PictureBoxSizeMode.StretchImage;
            pb_search.SizeMode = PictureBoxSizeMode.StretchImage;
            videoCapture = new VideoCapture(0, VideoCaptureAPIs.DSHOW);
            isCameraOn = false;
            stop = false;

            // TCP Client �ʱ�ȭ (IP �ּҿ� ��Ʈ ��ȣ�� ��������� ������ �°� ����)
            tcpClientHandler = new TcpClientHandler("172.30.1.43", 65432);

            // AWS Ŭ���̾�Ʈ �ʱ�ȭ
            awsClient = new AwsClient();

            // DatabaseHandler �ʱ�ȭ (MySQL ���� ���� ����)
            dbHandler = new DatabaseHandler("localhost", "my_database", "root", "1234");

            // DataGridView CellClick �̺�Ʈ �ڵ鷯 ���
            dataGridView1.CellClick += dataGridView1_CellClick;

            // HttpClient �ʱ�ȭ
            httpClient = new HttpClient();

        }


        //START Ŭ��
        private async void btn_start_Click(object sender, EventArgs e)
        {
            //ī�޶� ����������.
            if (!isCameraOn)
            {
                //// TCP ������ ����
                //try
                //{
                //    await tcpClientHandler.ConnectAsync();
                //    tcpClientHandler.SendMessageAsync("start");
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show(ex.Message);
                //    return;
                //}

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

        //
        private string AnalyzeColor(Mat image)
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
                return dominantColor;
            }
            return "Unknown";
        }

        //���� Ŭ�� �ε� ���� �������� �̺�Ʈ�� ������.
        private async void btn_del_Click(object sender, EventArgs e)
        {
            if (isCameraOn && videoCapture != null && videoCapture.IsOpened())
            {
                // ���� ������ ĸó�� ���� ���ο� Mat ��ü
                Mat captureMat = new Mat();

                // �񵿱������� ī�޶󿡼� �̹��� ĸó
                Task captureTask = Task.Run(() =>
                {
                    videoCapture.Read(captureMat);
                });

                // ĸó �۾��� �Ϸ�� ������ ���
                await captureTask;

                if (!captureMat.Empty())
                {
                    // ��Ʈ������ ��ȯ
                    var bitmap = BitmapConverter.ToBitmap(captureMat);

                    // ���ÿ� ���� ����
                    string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                    string imagePath = $"{timestamp}.jpg";
                    bitmap.Save(imagePath);

                    // AWS S3�� ���� ���ε�
                    await awsClient.UploadFileAsync(imagePath, imagePath);

                    // S3 URL ��������
                    string s3Url = awsClient.GetFileUrl(imagePath);

                    // ���� �м�
                    string dominantColor = AnalyzeColor(captureMat);

                    // ����(NG,OK)
                    string status = "NG";

                    //�ҷ� ����
                    string poor = "";

                    // DataGridView�� ������ �߰�
                    var photoRecord = new PhotoRecord
                    {
                        SequenceNumber = photoSequenceNumber++,
                        S3Url = s3Url,
                        DominantColor = dominantColor,
                        Status = status,
                        DateTaken = DateTime.Now
                    };

                    // DataGridView�� �� �� �߰�
                    dataGridView1.Rows.Add(
                        photoRecord.SequenceNumber,
                        photoRecord.S3Url,
                        photoRecord.DominantColor,
                        photoRecord.Status,
                        photoRecord.DateTaken
                    );

                    // MySQL �����ͺ��̽� OBJECT ���̺� ������ ����
                    try
                    {
                        dbHandler.InsertObject(photoRecord.SequenceNumber, "��ü��", photoRecord.S3Url, "type");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }

                    // MYSQL �����ͺ��̽� OBJECTLOG ���̺� ������ ����
                    try
                    {
                        dbHandler.InsertObjectLog(photoRecord.SequenceNumber, status, poor, photoRecord.DateTaken, photoRecord.DominantColor);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }

                    MessageBox.Show($"{imagePath} �� ���ε� �Ǿ����ϴ�.");
                }
                else
                {
                    MessageBox.Show("ī�޶�κ��� ������ �������� ���߽��ϴ�.");
                }

                captureMat.Release();
            }
            else
            {
                MessageBox.Show("ī�޶� ���� ���� �ʰų� �ʱ�ȭ���� �ʾҽ��ϴ�.");
            }
        }

        private async void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // ���� Ŭ���� ��츸 ó��
            if (e.RowIndex >= 0)
            {
                // Ŭ���� ������ URL ���� �����ɴϴ�.
                string imageUrl = dataGridView1.Rows[e.RowIndex].Cells["dgv_url"].Value.ToString();
                // ���õ� ���� ������ �����͸� �����ɴϴ�.
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                // �� ���� ���� �ؽ�Ʈ �ڽ��� �����մϴ�.
                tb_id.Text = selectedRow.Cells["dgv_id"].Value?.ToString();
                tb_url.Text = selectedRow.Cells["dgv_url"].Value?.ToString();
                tb_cou.Text = selectedRow.Cells["dgv_color"].Value?.ToString();
                tb_status.Text = selectedRow.Cells["dgv_status"].Value?.ToString();
                tb_date.Text = selectedRow.Cells["dgv_date"].Value?.ToString();


                // URL�� ����ִ� ���
                if (string.IsNullOrEmpty(imageUrl))
                {
                    MessageBox.Show("URL�� ��� �ֽ��ϴ�.");
                    return;
                }

                try
                {
                    // �̹����� �ٿ�ε��մϴ�.
                    var response = await httpClient.GetAsync(imageUrl);
                    response.EnsureSuccessStatusCode();
                    var imageBytes = await response.Content.ReadAsByteArrayAsync();

                    using (var ms = new MemoryStream(imageBytes))
                    {
                        // MemoryStream���� Bitmap ����
                        var image = Bitmap.FromStream(ms) as Bitmap;

                        // PictureBox�� �̹��� ����
                        pb_search.Image = image;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"�̹����� �ε��ϴ� �� ������ �߻��߽��ϴ�: {ex.Message}");
                }


            }
        }

        //�ٿ�ε�
        private void btn_load_Click(object sender, EventArgs e)
        {
            // ���� ���� ��ȭ���� ����
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV Files (*.csv)|*.csv";
                sfd.Title = "Save CSV File";
                sfd.FileName = "data.csv";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    // ������ ��ο� ���ϸ����� CSV ������ ����
                    string filePath = sfd.FileName;
                    SaveDataGridViewToCsv(dataGridView1, filePath);
                    MessageBox.Show("Data saved to CSV file successfully.");
                }
            }
        }

        private void SaveDataGridViewToCsv(DataGridView dataGridView, string filePath)
        {
            // CSV ���� �ۼ�
            StringBuilder csvContent = new StringBuilder();

            // Column Headers
            for (int i = 0; i < dataGridView.Columns.Count; i++)
            {
                csvContent.Append(dataGridView.Columns[i].HeaderText);
                if (i < dataGridView.Columns.Count - 1)
                {
                    csvContent.Append(",");
                }
            }
            csvContent.AppendLine();

            // Rows
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.IsNewRow) continue;

                for (int i = 0; i < dataGridView.Columns.Count; i++)
                {
                    // Escape quotes and commas
                    string cellValue = row.Cells[i].Value?.ToString().Replace("\"", "\"\"") ?? "";
                    csvContent.Append($"\"{cellValue}\"");

                    if (i < dataGridView.Columns.Count - 1)
                    {
                        csvContent.Append(",");
                    }
                }
                csvContent.AppendLine();
            }

            // Write CSV to file
            File.WriteAllText(filePath, csvContent.ToString(), Encoding.UTF8);
        }

        //PC �� �ϵ��� ��� ����
        private void btn_connect_Click(object sender, EventArgs e)
        {

        }
    }
}

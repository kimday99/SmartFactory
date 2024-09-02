using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Text;
using System.Net.Http;
using System;
using static OpenCvSharp.Stitcher;
using Newtonsoft.Json;
using IOFile = System.IO.File;


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

        private int trans_number = 1;


        // �����ͺ��̽� ���� ���� ����
        private string server = "localhost"; // �Ǵ� ���� ���� �ּ�
        private string database = "smartfactory";
        private string username = "root";
        private string password = "1234";

        public Main()
        {
            InitializeComponent();
            videoCapture = new VideoCapture(0, VideoCaptureAPIs.DSHOW);
            isCameraOn = false;
            stop = false;

            // AWS Ŭ���̾�Ʈ �ʱ�ȭ
            awsClient = new AwsClient();

            // DatabaseHandler �ʱ�ȭ (MySQL ���� ���� ����)
            dbHandler = new DatabaseHandler(server, database, username, password);


            // DataGridView CellClick �̺�Ʈ �ڵ鷯 ���
            dataGridView1.CellClick += dataGridView1_CellClick;

            // HttpClient �ʱ�ȭ
            httpClient = new HttpClient();

            
            pb_search.SizeMode = PictureBoxSizeMode.StretchImage;

        }


        //START Ŭ��
        private async void btn_start_Click(object sender, EventArgs e)
        {
            //ī�޶� ����������.
            if (!isCameraOn)
            {
                TcpClientHandler_SendMessage();
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
                videoCapture = new VideoCapture(1, VideoCaptureAPIs.DSHOW);
                //CameraCallback �޼��带 �񵿱������� �����Ͽ� ī�޶� ��Ʈ������ ó���մϴ�
                task = Task.Run(() => CameraCallback());
                isCameraOn = true;
                tcpClientHandler.MessageReceived += TcpClientHandler_MessageReceived;
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

        //��ķ �Կ� �޼���
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

        //histogram rgb ����
        private string AnalyzeColor(Mat image)
        {
            //���� ����
            string dominantColor = "";
            if (isCameraOn && !mat.Empty())
            {
                Mat StopImage = mat.Clone();

                Scalar mean = Cv2.Mean(StopImage);

                double blueMean = mean.Val0;
                double greenMean = mean.Val1;
                double redMean = mean.Val2;



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
                    pb_histo.BackColor = Color.Green;
                }
                else if (blueMean > redMean + threshold && blueMean > greenMean + threshold)
                {
                    dominantColor = "Blue";
                    pb_histo.BackColor = Color.Blue;
                }
                else
                {
                    dominantColor = "Unknown";
                    pb_histo.BackColor = (Color)Color.Black;
                }
            }
            return dominantColor;
        }


        private async void btn_del_Click(object sender, EventArgs e)
        {

        }



        //������ �׸��� �� �� Ŭ��
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

                //�� Ŭ���� PICTURE BOX �� URL �̹��� ����
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

        //�ٿ�ε� ��ư �̺�Ʈ
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


        //csv �ٿ�ε� �޼���
        //csv �ٿ�ε� �޼���
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
            System.IO.File.WriteAllText(filePath, csvContent.ToString());
        }



        //PC �� �ϵ��� ��� ����
        private void Main_Load(object sender, EventArgs e)
        {

        }


        //��������� ������ ����.
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
                    pb_hw.BackColor = Color.Green;
                    pb_sv.BackColor = Color.Green;
                    btn_start.Enabled = true;
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

        //�����̾� ��Ʈ �����?
        private void btn_logdel_Click(object sender, EventArgs e)
        {
            dgv_trans.Rows.Clear();
        }

        //�ϵ���� �۽�
        private void TcpClientHandler_SendMessage()
        {
            tcpClientHandler.SendMessageAsync("start");
            this.Invoke((Action)(() =>
            {
                dgv_trans.Rows.Add(trans_number++, "�۽�", "�����̾� �۵�", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            }));
        }


        //�ϵ���� �޼��� ����
        private async void TcpClientHandler_MessageReceived(string message)
        {
            //������ �޽����� DataGridView�� ���
            this.Invoke((Action)(() =>
            {
                dgv_trans.Rows.Add(trans_number++, "����", message, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            }));
            //�����̾Ʈ ���߰�.
            if (message == "picture")
            {
                await picture();
            }
        }

        //������� Ec2�� �̹��� ����
        private async Task picture()
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


                //���� �������
                if (!captureMat.Empty())
                {
                    // ��Ʈ������ ��ȯ
                    var bitmap = BitmapConverter.ToBitmap(captureMat);

                    // ���ÿ� ���� ����
                    string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                    string imagePath = $"{timestamp}.jpg";
                    //bitmap.Save(imagePath, System.Drawing.Imaging.ImageFormat.Bmp);
                    bitmap.Save(imagePath);


                    //AWS EC2�� ���� ����
                    awsClient = new AwsClient();                  
                    string responseJson = await awsClient.UploadImageToEC2(imagePath);


                    // ���� JSON �Ľ�
                    var responseObject = JsonConvert.DeserializeObject<dynamic>(responseJson);


                    // �����κ��� ������ ��ü�� S3 URL�� �޾ƿ�
                    string detectedObject = responseObject.detected_object;
                    string s3Url = responseObject.image_url;

                    // ���� ����
                    string status = "OK";

                    //���� �м�(�ڽ�) EC2 X
                    string dominantColor = AnalyzeColor(captureMat);


                    //�ҷ� ����(��ũ) 
                    if (dominantColor == "Unknown")
                    {
                        if (detectedObject == "Milk")
                        {
                            dominantColor = "Milk";
                            lb_okcnt.Text = (int.Parse(lb_okcnt.Text) + 1).ToString();
                        }
                        else if (detectedObject == "hole")
                        {
                            status = "NG";
                            dominantColor = "Hole";
                            lb_ngcnt_m.Text = (int.Parse(lb_ngcnt_m.Text) + 1).ToString();
                        }
                        else if (detectedObject == "Scratch")
                        {
                            status = "NG";
                            dominantColor = "Scratch";
                            lb_ngcnt_m.Text = (int.Parse(lb_ngcnt_m.Text) + 1).ToString();
                        }
                    }
                    lb_checkcnt_m.Text = (int.Parse(lb_okcnt.Text) + int.Parse(lb_ngcnt_m.Text)).ToString();
                    await tcpClientHandler.SendMessageAsync(dominantColor);


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

                    //�����ͺ��̽� ���� 
                    // **���⼭ �����ͺ��̽��� �����մϴ�.**
                    try
                    {
                        dbHandler.InsertObject(
                        photoRecord.SequenceNumber,
                        photoRecord.S3Url,
                        photoRecord.DominantColor,
                        photoRecord.Status,
                        photoRecord.DateTaken.ToString("yyyy-MM-dd HH:mm:ss")
                        );
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"�����ͺ��̽��� �����ϴ� �� ������ �߻��߽��ϴ�: {ex.Message}");
                    }

                    //�ٽ� �����̾� ��Ʈ ����
                    TcpClientHandler_SendMessage();



                }
            }
        }


        //
        private async void button1_Click(object sender, EventArgs e)
        {
            //await picture();
        }

        //�˻� ���� ��ư
        private void btn_adapt_Click(object sender, EventArgs e)
        {

            // ���õ� �޺��ڽ� �� ��������
            string selectedStatus = cb_status.Text;
            string selectedPoor = cb_poor.Text;

            // �����͸� �����ɴϴ�.
            List<Dictionary<string, object>> records = dbHandler.SelectObjects(status: selectedStatus, poor: selectedPoor);

            // DataGridView�� �ʱ�ȭ�մϴ�.
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            if (records.Count > 0)
            {
                // ù ��° ���ڵ��� Ű�� ������� �÷� ����
                foreach (var column in records[0].Keys)
                {
                    dataGridView1.Columns.Add(column, column);
                }

                // ������ �� �߰�
                foreach (var record in records)
                {
                    int rowIndex = dataGridView1.Rows.Add();
                    foreach (var column in record.Keys)
                    {
                        dataGridView1.Rows[rowIndex].Cells[column].Value = record[column];
                    }
                }
            }
            else
            {
                MessageBox.Show("No records found with the specified criteria.");
            }



        }
    }
}

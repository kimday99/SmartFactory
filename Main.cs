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
        // DatabaseHandler 객체
        private DatabaseHandler dbHandler;
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
        // AWS 클라이언트 추가
        private AwsClient awsClient;

        //ID 
        private int photoSequenceNumber = 1;

        private HttpClient httpClient;

        private int trans_number = 1;


        // 데이터베이스 연결 정보 설정
        private string server = "localhost"; // 또는 실제 서버 주소
        private string database = "smartfactory";
        private string username = "root";
        private string password = "1234";

        public Main()
        {
            InitializeComponent();
            videoCapture = new VideoCapture(0, VideoCaptureAPIs.DSHOW);
            isCameraOn = false;
            stop = false;

            // AWS 클라이언트 초기화
            awsClient = new AwsClient();

            // DatabaseHandler 초기화 (MySQL 연결 정보 설정)
            dbHandler = new DatabaseHandler(server, database, username, password);


            // DataGridView CellClick 이벤트 핸들러 등록
            dataGridView1.CellClick += dataGridView1_CellClick;

            // HttpClient 초기화
            httpClient = new HttpClient();

            
            pb_search.SizeMode = PictureBoxSizeMode.StretchImage;

        }


        //START 클릭
        private async void btn_start_Click(object sender, EventArgs e)
        {
            //카메라 꺼져있을시.
            if (!isCameraOn)
            {
                TcpClientHandler_SendMessage();
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
                videoCapture = new VideoCapture(1, VideoCaptureAPIs.DSHOW);
                //CameraCallback 메서드를 비동기적으로 실행하여 카메라 스트리밍을 처리합니다
                task = Task.Run(() => CameraCallback());
                isCameraOn = true;
                tcpClientHandler.MessageReceived += TcpClientHandler_MessageReceived;
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

        //웹캠 촬영 메서드
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

        //histogram rgb 구분
        private string AnalyzeColor(Mat image)
        {
            //지배 색깔
            string dominantColor = "";
            if (isCameraOn && !mat.Empty())
            {
                Mat StopImage = mat.Clone();

                Scalar mean = Cv2.Mean(StopImage);

                double blueMean = mean.Val0;
                double greenMean = mean.Val1;
                double redMean = mean.Val2;



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



        //데이터 그리드 뷰 셀 클릭
        private async void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // 행이 클릭된 경우만 처리
            if (e.RowIndex >= 0)
            {
                // 클릭된 셀에서 URL 값을 가져옵니다.
                string imageUrl = dataGridView1.Rows[e.RowIndex].Cells["dgv_url"].Value.ToString();
                // 선택된 행의 셀에서 데이터를 가져옵니다.
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                // 각 셀의 값을 텍스트 박스에 설정합니다.
                tb_id.Text = selectedRow.Cells["dgv_id"].Value?.ToString();
                tb_url.Text = selectedRow.Cells["dgv_url"].Value?.ToString();
                tb_cou.Text = selectedRow.Cells["dgv_color"].Value?.ToString();
                tb_status.Text = selectedRow.Cells["dgv_status"].Value?.ToString();
                tb_date.Text = selectedRow.Cells["dgv_date"].Value?.ToString();

                //셀 클릭시 PICTURE BOX 에 URL 이미지 띄우기
                try
                {
                    // 이미지를 다운로드합니다.
                    var response = await httpClient.GetAsync(imageUrl);
                    response.EnsureSuccessStatusCode();
                    var imageBytes = await response.Content.ReadAsByteArrayAsync();

                    using (var ms = new MemoryStream(imageBytes))
                    {
                        // MemoryStream에서 Bitmap 생성
                        var image = Bitmap.FromStream(ms) as Bitmap;

                        // PictureBox에 이미지 설정
                        pb_search.Image = image;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"이미지를 로드하는 중 오류가 발생했습니다: {ex.Message}");
                }
            }
        }

        //다운로드 버튼 이벤트
        private void btn_load_Click(object sender, EventArgs e)
        {
            // 파일 저장 대화상자 생성
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV Files (*.csv)|*.csv";
                sfd.Title = "Save CSV File";
                sfd.FileName = "data.csv";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    // 선택한 경로와 파일명으로 CSV 파일을 저장
                    string filePath = sfd.FileName;
                    SaveDataGridViewToCsv(dataGridView1, filePath);
                    MessageBox.Show("Data saved to CSV file successfully.");
                }
            }
        }


        //csv 다운로드 메서드
        //csv 다운로드 메서드
        private void SaveDataGridViewToCsv(DataGridView dataGridView, string filePath)
        {
            // CSV 파일 작성
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



        //PC 와 하드웨어간 통신 연결
        private void Main_Load(object sender, EventArgs e)
        {

        }


        //라즈베리파이 측으로 연결.
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

        //컨베이어 벨트 멈춤시?
        private void btn_logdel_Click(object sender, EventArgs e)
        {
            dgv_trans.Rows.Clear();
        }

        //하드웨어 송신
        private void TcpClientHandler_SendMessage()
        {
            tcpClientHandler.SendMessageAsync("start");
            this.Invoke((Action)(() =>
            {
                dgv_trans.Rows.Add(trans_number++, "송신", "컨베이어 작동", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            }));
        }


        //하드웨어 메세지 수신
        private async void TcpClientHandler_MessageReceived(string message)
        {
            //수신한 메시지를 DataGridView에 출력
            this.Invoke((Action)(() =>
            {
                dgv_trans.Rows.Add(trans_number++, "수신", message, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            }));
            //컨베이어벨트 멈추고.
            if (message == "picture")
            {
                await picture();
            }
        }

        //사진찍고 Ec2에 이미지 전송
        private async Task picture()
        {
            if (isCameraOn && videoCapture != null && videoCapture.IsOpened())
            {
                // 현재 프레임 캡처를 위한 새로운 Mat 객체
                Mat captureMat = new Mat();

                // 비동기적으로 카메라에서 이미지 캡처
                Task captureTask = Task.Run(() =>
                {
                    videoCapture.Read(captureMat);
                });

                // 캡처 작업이 완료될 때까지 대기
                await captureTask;


                //사진 찍었을때
                if (!captureMat.Empty())
                {
                    // 비트맵으로 변환
                    var bitmap = BitmapConverter.ToBitmap(captureMat);

                    // 로컬에 사진 저장
                    string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                    string imagePath = $"{timestamp}.jpg";
                    //bitmap.Save(imagePath, System.Drawing.Imaging.ImageFormat.Bmp);
                    bitmap.Save(imagePath);


                    //AWS EC2에 사진 전송
                    awsClient = new AwsClient();                  
                    string responseJson = await awsClient.UploadImageToEC2(imagePath);


                    // 응답 JSON 파싱
                    var responseObject = JsonConvert.DeserializeObject<dynamic>(responseJson);


                    // 서버로부터 감지된 객체와 S3 URL을 받아옴
                    string detectedObject = responseObject.detected_object;
                    string s3Url = responseObject.image_url;

                    // 상태 결정
                    string status = "OK";

                    //색상 분석(박스) EC2 X
                    string dominantColor = AnalyzeColor(captureMat);


                    //불량 내용(밀크) 
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


                    // DataGridView에 데이터 추가
                    var photoRecord = new PhotoRecord
                    {
                        SequenceNumber = photoSequenceNumber++,
                        S3Url = s3Url,
                        DominantColor = dominantColor,
                        Status = status,
                        DateTaken = DateTime.Now
                    };

                    // DataGridView에 새 행 추가
                    dataGridView1.Rows.Add(
                        photoRecord.SequenceNumber,
                        photoRecord.S3Url,
                        photoRecord.DominantColor,
                        photoRecord.Status,
                        photoRecord.DateTaken
                    );

                    //데이터베이스 저장 
                    // **여기서 데이터베이스에 저장합니다.**
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
                        MessageBox.Show($"데이터베이스에 저장하는 중 오류가 발생했습니다: {ex.Message}");
                    }

                    //다시 컨베이어 벨트 시작
                    TcpClientHandler_SendMessage();



                }
            }
        }


        //
        private async void button1_Click(object sender, EventArgs e)
        {
            //await picture();
        }

        //검색 적용 버튼
        private void btn_adapt_Click(object sender, EventArgs e)
        {

            // 선택된 콤보박스 값 가져오기
            string selectedStatus = cb_status.Text;
            string selectedPoor = cb_poor.Text;

            // 데이터를 가져옵니다.
            List<Dictionary<string, object>> records = dbHandler.SelectObjects(status: selectedStatus, poor: selectedPoor);

            // DataGridView를 초기화합니다.
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            if (records.Count > 0)
            {
                // 첫 번째 레코드의 키를 기반으로 컬럼 생성
                foreach (var column in records[0].Keys)
                {
                    dataGridView1.Columns.Add(column, column);
                }

                // 데이터 행 추가
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

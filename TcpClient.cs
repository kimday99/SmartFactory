using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

public class TcpClientHandler : IDisposable
{
    private TcpClient tcpClient;
    private string serverIp;
    private int serverPort;

    // 메시지 수신 이벤트 추가
    public event Action<string> MessageReceived;

    public TcpClientHandler(string ip, int port)
    {
        serverIp = ip;
        serverPort = port;
        tcpClient = new TcpClient();
    }

    // 서버에 연결
    public async Task ConnectAsync()
    {
        if (tcpClient.Connected)
        {
            return;  // 이미 연결된 상태라면 재연결하지 않음
        }

        try
        {
            await tcpClient.ConnectAsync(serverIp, serverPort);
            
            // 서버에 연결되면 메시지 수신 시작
            _ = ReceiveMessagesAsync();

        }
        catch (Exception ex)
        {
            throw new Exception($"Unable to connect to server: {ex.Message}");
        }
    }

    // 메시지 전송
    public async Task SendMessageAsync(string message)
    {
        if (tcpClient != null && tcpClient.Connected)
        {
            try
            {
                NetworkStream stream = tcpClient.GetStream();
                byte[] data = Encoding.UTF8.GetBytes(message);
                await stream.WriteAsync(data, 0, data.Length);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to send message: {ex.Message}");
            }
        }
        else
        {
            throw new InvalidOperationException("TCP client is not connected.");
        }
    }

    // 메시지 수신 (비동기)
    private async Task ReceiveMessagesAsync()
    {
        try
        {
            NetworkStream stream = tcpClient.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead;

            while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                string receivedMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                // 메시지를 수신하면 이벤트를 통해 알림
                MessageReceived?.Invoke(receivedMessage);
                //MessageBox.Show(receivedMessage);
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to receive message: {ex.Message}");
        }
    }

    // 연결 종료
    public void Disconnect()
    {
        if (tcpClient != null && tcpClient.Connected)
        {
            tcpClient.Close();
            tcpClient = null;
        }
    }

    // IDisposable 구현
    public void Dispose()
    {
        Disconnect();
        tcpClient?.Dispose();
    }
}

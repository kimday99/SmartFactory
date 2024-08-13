using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

public class TcpClientHandler : IDisposable
{
    private TcpClient tcpClient;
    private string serverIp;
    private int serverPort;

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

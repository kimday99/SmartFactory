using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class AwsClient
{
    private readonly HttpClient client;
    // EC2 서버 URL
    private readonly string _ec2Url = "http://127.0.0.1:5000/upload";

    public AwsClient()
    {
        MessageBox.Show("생성자");
        client = new HttpClient();
    }

    public async Task<string> UploadImageToEC2(string imagePath)
    {
        MessageBox.Show("메서드");
        using (var content = new MultipartFormDataContent())
        {
            var fileContent = new ByteArrayContent(File.ReadAllBytes(imagePath));
            MessageBox.Show("1");
            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");
            MessageBox.Show("2");
            content.Add(fileContent, "file", Path.GetFileName(imagePath));
            MessageBox.Show("3");

            var response = await client.PostAsync(_ec2Url, content);
            MessageBox.Show("4");
            response.EnsureSuccessStatusCode();
            MessageBox.Show("5");
            return await response.Content.ReadAsStringAsync();
        }
    }    
}

using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class AwsClient
{
    private readonly HttpClient client;
    // EC2 서버 URL
    private readonly string _ec2Url = "http://127.0.0.1:5100/upload";

    public AwsClient()
    {
        client = new HttpClient();
    }

    public async Task<string> UploadImageToEC2(string imagePath)
    {
        using (var content = new MultipartFormDataContent())
        {
            var fileContent = new ByteArrayContent(File.ReadAllBytes(imagePath));

            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpg");

            content.Add(fileContent, "file", Path.GetFileName(imagePath));

            var response = await client.PostAsync(_ec2Url, content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }    
}

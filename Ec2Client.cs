using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.IO;

public class Ec2Client
{
    private readonly string _ec2UploadUrl;

    public Ec2Client(string ec2UploadUrl)
    {
        _ec2UploadUrl = ec2UploadUrl;
    }

    public async Task UploadImageToEc2Async(string filePath)
    {
        using (var client = new HttpClient())
        {
            using (var form = new MultipartFormDataContent())
            {
                var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                var fileContent = new StreamContent(fileStream);
                fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
                form.Add(fileContent, "file", Path.GetFileName(filePath));

                try
                {
                    var response = await client.PostAsync(_ec2UploadUrl, form);
                    response.EnsureSuccessStatusCode();
                    Console.WriteLine("Image uploaded to EC2 successfully.");
                    var responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Response from EC2: " + responseBody);
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Request error: {e.Message}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Unknown error: {e.Message}");
                }
            }
        }
    }
}



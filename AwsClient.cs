using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using System;
using System.Threading.Tasks;

public class AwsClient
{
    // S3 버킷 이름
    private const string bucketName = "";
    // 서울 리전 설정
    private static readonly RegionEndpoint bucketRegion = RegionEndpoint.APNortheast2;  

    private IAmazonS3 s3Client;

    public AwsClient()
    {
        // AWS 자격 증명과 함께 클라이언트 생성
        s3Client = new AmazonS3Client("", "", bucketRegion);
    }

    public async Task UploadFileAsync(string filePath, string keyName)
    {
        try
        {
            var fileTransferUtility = new TransferUtility(s3Client);

            // 파일 업로드
            await fileTransferUtility.UploadAsync(filePath, bucketName, keyName);
            Console.WriteLine("Upload completed");
        }
        catch (AmazonS3Exception e)
        {
            Console.WriteLine($"Error encountered on server. Message:'{e.Message}' when writing an object");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Unknown encountered on server. Message:'{e.Message}' when writing an object");
        }
    }

    public string GetFileUrl(string keyName)
    {
        return $"https://{bucketName}.s3.{bucketRegion.SystemName}.amazonaws.com/{keyName}";
    }
}

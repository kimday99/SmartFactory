
public class PhotoRecord
{
    public int SequenceNumber { get; set; }
    public string S3Url { get; set; }
    public string DominantColor { get; set; }
    public string Status { get; set; }
    public DateTime DateTaken { get; set; }
    public string Poor { get; set; }

    public override string ToString()
    {
        return $"{SequenceNumber}: {S3Url} | {DominantColor} | {Status} | {DateTaken}";
    }
}

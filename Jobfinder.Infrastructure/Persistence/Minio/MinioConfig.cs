namespace Jobfinder.Infrastructure.Persistence.Minio;

public class MinioConfig
{
    public required string ServiceUrl { get; set; }
    public required bool ForcePathStyle { get; set; }
    public required int TimeOutInSecond { get; set; }
    public required int  MaxErrorRetry { get; set; }
    public required bool UseHttp { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }

}
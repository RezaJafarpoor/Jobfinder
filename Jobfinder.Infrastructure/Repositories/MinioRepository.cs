using Amazon.S3;
using Amazon.S3.Model;
using Jobfinder.Application.Interfaces.Repositories;

namespace Jobfinder.Infrastructure.Repositories;

public class MinioRepository
    (IAmazonS3 s3):  IMinioRepository

{
    public async Task<PutObjectResponse> UploadFileAsync( string key, Stream fileStream)
    {
        var putRequest = new PutObjectRequest
        {
            BucketName = "job-seeker",
            Key = key,
            InputStream = fileStream
        };
        var result = await s3.PutObjectAsync(putRequest);
        return result;
    }

    public async Task<Stream> DownloadFileAsync( string key)
    {
        var getRequest = new GetObjectRequest
        {
            BucketName = "job-seeker",
            Key = key
        };
        var result = await s3.GetObjectAsync(getRequest);
        return result.ResponseStream;
    }
}
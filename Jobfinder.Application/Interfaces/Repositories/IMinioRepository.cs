using Amazon.S3.Model;

namespace Jobfinder.Application.Interfaces.Repositories;

public interface IMinioRepository
{
    Task<PutObjectResponse> UploadFileAsync(string key, Stream fileStream);
    Task<Stream> DownloadFileAsync(string key);

}
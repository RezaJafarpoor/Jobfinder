using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace Jobfinder.Infrastructure.Seeds;

public class S3Seed(IAmazonS3 s3)
{
    public async Task<bool> CheckBucketAndCreate(string bucketName)
    {

        var response = await s3.ListBucketsAsync();
        if (response.Buckets.Any(b => b.BucketName == bucketName))
            return false;
        var result = await s3.PutBucketAsync(bucketName);
        if (result.HttpStatusCode is HttpStatusCode.OK)
            return true;
        return false;
    }

}
        

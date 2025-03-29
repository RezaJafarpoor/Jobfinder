using Jobfinder.Api.Endpoints;

namespace Jobfinder.Api.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddApiServices(this IServiceCollection services)
    {
        
    }

    public static void AddEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.AddIdentityEndpoints();
        builder.AddCvEndpoints();
    }
}
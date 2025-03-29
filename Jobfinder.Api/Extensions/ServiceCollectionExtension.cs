using Jobfinder.Api.Endpoints;
using Jobfinder.Api.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Jobfinder.Api.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddApiServices(this IServiceCollection services)
    {
        services.Configure<JsonOptions>(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonDateConverter());
        });
    }

    public static void AddEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.AddIdentityEndpoints();
        builder.AddCvEndpoints();
    }
}
using Jobfinder.Application.Dtos.Cv;
using Jobfinder.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace Jobfinder.Api.Endpoints;

public static class CvEndpoints
{
    public static void AddCvEndpoints(this IEndpointRouteBuilder builder)
    {
        var root = builder.MapGroup("api");

        root.MapPost("cv", async ([FromBody] CreateCvDto createCvDto, CvService service, HttpContext context, CancellationToken cancellationToken)
            =>
        {
            var subClaim = context.User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
            if (Guid.TryParse(subClaim, out Guid userId))
            {
                var result =await service.CreateCv(createCvDto, userId, cancellationToken);
                return result.IsSuccess ? Results.Ok(result.Data) : Results.BadRequest();
            }

            return Results.Unauthorized();
        }).RequireAuthorization();
    }
}
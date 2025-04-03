using Jobfinder.Application.Dtos.Company;
using Jobfinder.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Jobfinder.Api.Endpoints;

public static class CompanyEndpoints
{
    public static void AddCompanyEndpoints(this IEndpointRouteBuilder builder)
    {
        var root = builder.MapGroup("api");

        root.MapPost("company", async ([FromBody]CreateCompanyDto dto,HttpContext context, CancellationToken cancellationToken, EmployerService service) =>
        {
            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Guid.TryParse(userId, out Guid employerId);
            var result = await service.CreateCompany(employerId, dto, cancellationToken);
            return result.IsSuccess
                ? Results.NoContent()
                : Results.BadRequest(result.Errors);
        
        });

    }
}
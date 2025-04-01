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


        root.MapPost("company", async ([FromBody]CreateCompanyDto dto,HttpContext context, CompanyService service,CancellationToken cancellationToken) =>
        {
            var employer = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (employer is null)
                return Results.Unauthorized();
            Guid.TryParse(employer, out Guid employerId);
            var result= await service.AddCompanyForEmployer(employerId, dto, cancellationToken);
            return result.IsSuccess ? 
                Results.NoContent() :
                Results.BadRequest(result.Errors);
        });


        root.MapPut("company", async ([FromBody]UpdateCompanyDto dto, HttpContext context, CompanyService service, CancellationToken cancellationToken) =>
        {
            var employer = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Guid.TryParse(employer, out Guid employerId);
            var result = await service.UpdateCompanyForEmployer(employerId, dto, cancellationToken);
            return result.IsSuccess ?
                Results.NoContent() : 
                Results.BadRequest(result.Errors);
        });
    }
}
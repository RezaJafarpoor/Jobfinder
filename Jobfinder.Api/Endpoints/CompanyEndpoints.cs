using Jobfinder.Application.Dtos.Company;
using Jobfinder.Application.Interfaces.Repositories;
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

        root.MapGet("company", async (CancellationToken cancellationToken, ICompanyRepository repository) =>
        {
            var companies = await repository.GetCompanies(cancellationToken);
            var dtos = companies.Select(c => (CompanyDto) c);
        });

        root.MapGet("company/{id}", async ([FromRoute] string id, CancellationToken cancellationToken,
            ICompanyRepository repository) =>
        {
            Guid.TryParse(id, out Guid companyId);
            var company = await repository.GetCompanyById(companyId, cancellationToken);
            if (company is null)
                return Results.NotFound();
            CompanyDto dto = company;
            return Results.Ok(dto);
        });
    }
}
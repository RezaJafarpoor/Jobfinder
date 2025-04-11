using  Jobfinder.Application.Dtos.Identity;
using Jobfinder.Application.Services;
using Jobfinder.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Jobfinder.Api.Endpoints;

public static class IdentityEndpoints
{
    public static void AddIdentityEndpoints(this IEndpointRouteBuilder builder)
    {
        var root = builder.MapGroup("api/identity");

        root.MapPost("register",
            async ([FromBody] RegisterDto register, RegisterService registerService, CancellationToken cancellationToken) =>
            {
                var result = await registerService.Register(register, cancellationToken);
                return result.Errors.Count == 0 ? 
                    Results.Ok(result.Data) 
                    : Results.BadRequest(result.Errors);
            });
        

        root.MapPost("login", async ([FromBody]LoginDto loginDto, LoginService loginService, CancellationToken cancellationToken) =>
        {
            var result = await loginService.LoginWithPassword(loginDto, cancellationToken);
           return  result.Errors.Count == 0 ?
               Results.Ok(result.Data) :
               Results.BadRequest(result.Errors);
        });

        root.MapPost("refresh", async ([FromBody] string token, RefreshService service,
            CancellationToken cancellationToken) =>
        {
            var result = await service.CheckRefreshToken(token, cancellationToken);
            return result.IsSuccess ? 
                Results.Ok(result.Data) : 
                Results.BadRequest(result.Errors);
        });


        root.MapPost("forget", async ([FromBody] ForgetPasswordDto dto, ResetPasswordService service,
            CancellationToken cancellationToken) =>
        {
            var result = await service.ForgetPassword(dto.Email, cancellationToken);
            return result.IsSuccess ? 
                Results.Ok(result.Data) : 
                Results.BadRequest(result.Errors);
        });

        root.MapPost("reset", async ([FromBody]ResetPasswordDto dto,ResetPasswordService service,
            CancellationToken cancellationToken) =>
        {
            var result = await service.ResetPassword(dto.Email, dto.NewPassword, dto.Token);
            return result.IsSuccess ? 
                Results.Ok(result.Data) : 
                Results.BadRequest(result.Errors);
        });
    }
}


        

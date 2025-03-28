﻿using Jobfinder.Application.Interfaces;
using Jobfinder.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Jobfinder.Application.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IRegisterService, RegisterService>();
    }
}
using Microsoft.Extensions.DependencyInjection;
using TFG.Application.Contracts.Service;
using TFG.Application.Features.Service;

namespace TFG.Application;

public static class ApplicationServices 
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ICustomerService,CustomerService>();
        return services;
    }
}
using Microsoft.Extensions.DependencyInjection;
using TFG.Application.Contracts.Service;
using TFG.Application.Features.Service;

namespace TFG.Application;

public static class ApplicationServices 
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ICustomerService,CustomerService>();
        services.AddScoped<ILoginService,LoginService>();
        services.AddScoped<IRoleService,RoleService>();
        return services;
    }
}
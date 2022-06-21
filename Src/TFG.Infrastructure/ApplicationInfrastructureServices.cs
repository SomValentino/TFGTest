using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TFG.Application.Contracts.Persistence;
using TFG.Domain.Entities;
using TFG.Infrastructure.Data;
using TFG.Infrastructure.Repository;

namespace TFG.Infrastructure;

public static class ApplicationInfrastructureServices {
    public static IServiceCollection AddApplicationInfrastructureServices (this IServiceCollection services,
        IConfiguration configuration) {
        services.AddScoped<IDataContext<Customer>, CustomerDataContext> (options => {

            return new CustomerDataContext (configuration["DatabaseSettings:ConnectionString"],
                configuration["DatabaseSettings:DatabaseName"], configuration["DatabaseSettings:CollectionName"]);
        });

        services.AddScoped<ICustomerRepository, CustomerRepository> ();

        return services;
    }
}
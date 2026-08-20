using ClearBank.DemoFramework.Data;
using ClearBank.DemoFramework.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ClearBank.DemoFramework;

public static class DependencyInjection
{
    public static IServiceCollection AddClearBankServices(this IServiceCollection services)
    {
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IDataStoreService, DataStoreService>();
        services.AddScoped<IAccountDataStore, AccountDataStore>();

        return services;
    }
}
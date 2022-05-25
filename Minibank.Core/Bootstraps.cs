using Microsoft.Extensions.DependencyInjection;
using Minibank.Core.Domains.AccountsBank.Services;
using Minibank.Core.Domains.Users.Services;


namespace Minibank.Core
{
    public static class Bootstraps
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddScoped<ICalculator, Calculator>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAccountBankService, AccountBankService>();
            return services;
        }
    }
}

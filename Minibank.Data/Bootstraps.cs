using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minibank.Core;
using Minibank.Core.Domains.AccountsBank.Repositories;
using Minibank.Core.Domains.Users.Repositories;
using Minibank.Data.AccountsBank;
using Minibank.Data.Users.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Minibank.Data
{
    public static class Bootstraps
    {
        public static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<IDatabase, Database>(options =>
            {
                options.BaseAddress = new Uri(configuration["SomeUri"]);
            });
            services.AddScoped<IAccountBankRepository, AccountBankRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddDbContext<MinibankContext>(options => options
                //.UseLazyLoadingProxies()
                .UseNpgsql("Host=localhost;Port=5432;Database=minibank;Username=postgres;Password=Wasalas2003"));
            return services;
        }
    }
}

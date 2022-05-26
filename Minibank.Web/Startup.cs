using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Minibank.Core;
using Minibank.Data;
using Minibank.Data.HttpCLients;
using Minibank.Web.HostedServices;
using Minibank.Web.Middlewares;

namespace Minibank.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo {Title = "Minibank.Web", Version = "v1"});
            //});
            //services.AddHttpClient<IDatabase, Database>(options =>
            //{
            //    options.BaseAddress = new Uri("https://www.cbr-xml-daily.ru");
            //});
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Fintech.Web", Version = "v1" });
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
            services.AddOptions<ApplicationSettings>("MyService");
            services.AddHostedService<MigrationHostedService>();
            services
                .AddData(Configuration)
                .AddCore();

            //services.AddScoped<IDatabase, Database>();
            //services.AddScoped<ICalculator, Calculator>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minibank.Web v1"));
            }
            app.UseMiddleware<ValidationException>();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
using Demo.AzureConfig.Customers.Api.API.Services;
using Demo.AzureConfig.Customers.Core;
using Demo.AzureConfig.Customers.Infrastructure;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.FeatureManagement;
using Bootstrapper = Demo.AzureConfig.Customers.Core.Bootstrapper;

namespace Demo.AzureConfig.Customers.Api
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
            services.AddAzureAppConfiguration()
                .RegisterCore()
                .RegisterInfrastructure(Configuration)
                .AddFeatureManagement();

            RegisterApiServices(services);
            RegisterMediators(services);
            RegisterValidators(services);
        }

        private void RegisterApiServices(IServiceCollection services)
        {
            services.AddScoped<ICustomerService, CustomerService>();
        }

        private void RegisterMediators(IServiceCollection services)
        {
            var assemblies = new[]
            {
                typeof(Startup).Assembly,
                typeof(Bootstrapper).Assembly,
                typeof(Infrastructure.Bootstrapper).Assembly
            };

            services.AddMediatR(assemblies, configuration => { configuration.AsScoped(); });
        }

        private void RegisterValidators(IServiceCollection services)
        {
            var assemblies = new[]
            {
                typeof(Startup).Assembly,
                typeof(Bootstrapper).Assembly,
                typeof(Infrastructure.Bootstrapper).Assembly
            };

            services.AddValidatorsFromAssemblies(assemblies);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAzureAppConfiguration();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
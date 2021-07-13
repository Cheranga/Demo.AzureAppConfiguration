using Demo.AzureConfig.Customers.Api.Configs;
using Demo.AzureConfig.Customers.Api.DataAccess;
using Demo.AzureConfig.Customers.Api.DataAccess.Commands;
using Demo.AzureConfig.Customers.Api.DataAccess.Models;
using Demo.AzureConfig.Customers.Api.DataAccess.Queries;
using Demo.AzureConfig.Customers.Api.Models;
using Demo.AzureConfig.Customers.Api.Services;
using Demo.AzureConfig.Customers.Api.Validators;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.FeatureManagement;

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
            services.AddAzureAppConfiguration();
            services.AddFeatureManagement();

            RegisterConfigurations(services);
            RegisterDataAccess(services);
            RegisterCoreServices(services);
            RegisterMediators(services);
            RegisterValidators(services);
        }

        private void RegisterDataAccess(IServiceCollection services)
        {
            services.AddSingleton<ITableStorageFactory, StorageTableFactory>();
            // Query handlers
            services.AddScoped<IQueryHandler<SearchCustomerByIdQuery, CustomerDataModel>, SearchCustomerByIdQueryHandler>();
            // Command handlers
            services.AddScoped<ICommandHandler<CreateCustomerCommand>, CreateCustomerCommandHandler>();
        }

        private void RegisterConfigurations(IServiceCollection services)
        {
            services.AddSingleton(provider =>
            {
                var tableConfiguration = Configuration.GetSection(nameof(StorageTableConfiguration)).Get<StorageTableConfiguration>();
                return tableConfiguration;
            });
        }

        private void RegisterCoreServices(IServiceCollection services)
        {
            services.AddScoped<ICustomerService, CustomerService>();
        }

        private void RegisterMediators(IServiceCollection services)
        {
            var assemblies = new[]
            {
                typeof(Startup).Assembly
            };

            services.AddMediatR(assemblies, configuration =>
            {
                configuration.AsScoped();
            });
        }

        private void RegisterValidators(IServiceCollection services)
        {
            var assemblies = new[]
            {
                typeof(Startup).Assembly
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
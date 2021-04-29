using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoanCalculationService.Interfaces;
using LoanCalculationService.Model;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace LoanCalculationService
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
            var settings = Configuration.GetSection("LoanSettings").Get<LoanSettings>();
            services.AddSingleton(settings);
            services.AddSingleton(Configuration.GetSection("LoanFeeSettings").Get<LoanFeeSettings>());
            services.AddSingleton<ILoanFeeCalculator, LoanFeeCalculator>();
            services.AddSingleton<ILoanInterestCalculator, LoanInterestCalculator>();
            services.AddControllers();

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo { Title = "LoanCalculation" });
                x.ExampleFilters();
            });

            services.AddSwaggerExamplesFromAssemblyOf<Startup>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var swaggerOptions = new SwaggerOptions();
            Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);


            app.UseSwagger(option =>
            {
                option.RouteTemplate = swaggerOptions.JsonRoute;

            });

            app.UseSwaggerUI(option =>
            {
                option.SwaggerEndpoint(swaggerOptions.UiEndpoint, swaggerOptions.Description);

            });
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

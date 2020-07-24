using System;
using System.Reflection;
using AutoMapper;
using Location;
using Location.ResponseModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Worker.Api.Configuration;
using Worker.DAL;
using Worker.Location;

namespace Worker.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddHealthChecks();
            services.AddSwaggerGen();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddTransient<IApiKey, GoogleLocationApiKey>();
            services.AddHttpClient<GeocodingService>()
                .AddPolicyHandler(PollyConfig.GetRetryPolicy())
                .AddPolicyHandler(PollyConfig.GetCircuitBreakerPolicy());
                
            services.AddDbContext<WorkerDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("WorkerDatabase")));
            services.AddScoped<IWorkerRepository, WorkerRepository>();
            services.AddTransient<IAddressToCoordinatesTranslator, AddressToCoordinatesTranslator>();
            services.AddTransient<DistanceCalculator>();
            services.AddTransient<WorkerProfileFinder>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
                options.SwaggerEndpoint("/swagger/v1/swagger.json", Assembly.GetExecutingAssembly().FullName));

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/hc");
            });
        }

    }
}

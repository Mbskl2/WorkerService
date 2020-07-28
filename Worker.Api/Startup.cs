using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Location;
using Location.ResponseModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Worker.Api.Configuration;
using Worker.Api.Configuration.AuthZero;
using Worker.Api.Configuration.ErrorHandling;
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
            services.AddSwaggerGen(c =>
            {
                var xmlPath = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
                c.IncludeXmlComments(xmlPath);
            });
            services.AddAuthZeroConfig(Configuration);
            services.AddCors(CorsConfig.GetPolicy());

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

            services.AddSpaStaticFiles(SpaConfig.GetOptions());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseErrorHandlerConfig();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
                options.SwaggerEndpoint("/swagger/v1/swagger.json", Assembly.GetExecutingAssembly().FullName));

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/hc");
            });

            app.UseSpaConfig(env.IsDevelopment());
        }
    }
}

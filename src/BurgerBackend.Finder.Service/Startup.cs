using System;
using BurgerBackend.Finder.Service.Config;
using BurgerBackend.Finder.Service.ExternalServices;
using BurgerBackend.Finder.Service.Repositories;
using BurgerBackend.Identity.Client.Middlewares;
using BurgerBackend.Identity.Client.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace BurgerFinderService
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services) => services
            .AddControllers()
            .Services
            .AddSingleton<IBurgerShopRepository, BurgerShopRepository>() // TODO singleton because it's in memory, should be refactored to a real repository
            .AddScoped<IBurgerShopService, BurgerShopService>() 
            .Configure<AppSettings>(Configuration.GetSection(nameof(AppSettings)))
            .AddCustomSwagger()
            .AddHttpClient<IAccountService, AccountService>((sp, cl) => cl.BaseAddress = new Uri(sp.GetRequiredService<IOptions<AppSettings>>().Value.IdentityServiceUrl));

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BurgerFinderService v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    internal static class CustomStartupExtensions
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "BurgerBackend",
                    Version = "v1",
                    Description = "The burger finder API"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header \r\n Enter 'Bearer' [space] and your token",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            return services;
        }
    }
}

using BurgerBackend.Identity.Service.Config;
using BurgerBackend.Identity.Service.Middlewares;
using BurgerBackend.Identity.Service.Services;
using IdentityService.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace IdentityService
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services) => services
            .Configure<JwtConfig>(Configuration.GetSection(nameof(JwtConfig)))
            .AddSingleton<IUserInMemoryRespository, UserInMemoryRespository>() // TODO singleton because it's in memory, should be refactored to a real repository
            .AddScoped<ITokenService, TokenService>()
            .AddScoped<IUserService, UserService>()
            .AddControllers()
            .Services
            .AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BurgerBackend Identity Service", Version = "v1" });
            });

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "IdentityService v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            
            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}

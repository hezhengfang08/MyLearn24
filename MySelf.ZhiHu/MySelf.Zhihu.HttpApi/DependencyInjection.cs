using Microsoft.OpenApi.Models;
using MySelf.Zhihu.HttpApi.Infrastructure;
using MySelf.Zhihu.HttpApi.Services;
using MySelf.Zhihu.UseCases.Common.Interfaces;

namespace MySelf.Zhihu.HttpApi
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUser, CurrentUser>();

            services.AddHttpContextAccessor();

            services.AddExceptionHandler<UseCaseExceptionHandler>();
            services.AddProblemDetails();
            ConfigureSwagger(services);
            ConfigureCors(services);
            return services;
        }

        public static void ConfigureSwagger(IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(option =>
            {
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "JWT Bearer 认证",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                        }
                    },
                    new string[] { }
                     }
                });
            });
        }
        public static void ConfigureCors(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAny", builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
        }
    }
}

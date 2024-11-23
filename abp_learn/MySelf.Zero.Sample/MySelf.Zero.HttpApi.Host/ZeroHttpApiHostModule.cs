using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using MySelf.Zero.Application;
using MySelf.Zero.EntityFrameworkCore;
using MySelf.Zero.HttpApi.Host.Handlers;
using System.Text;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.AntiForgery;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace MySelf.Zero.HttpApi.Host
{
    [DependsOn(typeof(AbpAspNetCoreMvcModule), typeof(AbpAutofacModule))]
    [DependsOn(typeof(ZeroApplicationModule),
     typeof(ZeroEntityFrameworkCoreModule)
 )]
    public class ZeroHttpApiHostModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var services = context.Services;
            var configuration = services.GetConfiguration();

            // 跨域
            context.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            // 自动生成控制器
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(ZeroApplicationModule).Assembly);
            });

            // ABP The required antiforgery cookie
            Configure<AbpAntiForgeryOptions>(options =>
            {
                options.TokenCookie.SameSite = SameSiteMode.Lax;
                options.TokenCookie.Expiration = TimeSpan.FromDays(365);
                options.AutoValidateIgnoredHttpMethods.Add("POST");
            });

            // swagger
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "ForumApi",
                    Version = "v0.1"
                });
                options.DocInclusionPredicate((docName, predicate) => true);
                options.CustomSchemaIds(type => type.FullName);
            });

            services.AddDistributedRedisCache(r =>
            {
                r.Configuration = configuration["Redis:ConnectionString"];
            });

            ConfigureAuthentication(context);
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var env = context.GetEnvironment();
            var app = context.GetApplicationBuilder();
            var configuration = context.GetConfiguration();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowAll");
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "ForumApi");
            });

            app.UseRouting();
            app.UseAuthorization();
            app.UseAuthentication();
            app.UseConfiguredEndpoints();
        }

        private void ConfigureAuthentication(ServiceConfigurationContext context)
        {
            var services = context.Services;
            var configuration = services.GetConfiguration();
            string issuer = configuration["Jwt:Issuer"];
            string audience = configuration["Jwt:Audience"];
            string expire = configuration["Jwt:ExpireMinutes"];
            TimeSpan expiration = TimeSpan.FromMinutes(Convert.ToDouble(expire));
            SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecurityKey"]));

            services.AddAuthorization(options =>
            {
                // 1、Definition authorization policy
                options.AddPolicy("Permission", policy => policy.Requirements.Add(new PolicyRequirement()));
            }).AddAuthentication(s =>
            {
                // 2、Authentication
                s.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                s.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                s.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(s =>
            {
                // 3、Use Jwt bearer 
                s.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = key,
                    ClockSkew = expiration,
                    ValidateLifetime = true
                };
                s.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        // Token expired
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            // DI handler process function
            services.AddSingleton<IAuthorizationHandler, PolicyHandler>();

        }
    }
}
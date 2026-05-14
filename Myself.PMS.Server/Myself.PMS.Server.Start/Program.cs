
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Myself.PMS.Server.IService;
using Myself.PMS.Server.Service;
using SqlSugar;
using System.Text;

namespace Myself.PMS.Server.Start
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // 注册接口与实现
            RegistarClasses(builder.Services);
            // 配置鉴权
            ConfigAuthentication(builder.Services);


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
        private static void RegistarClasses(IServiceCollection services)
        {
            services.AddTransient<ISqlSugarClient>(client =>
            {
                //string connStr = "Data Source=localhost;Initial Catalog=PMS2024;User ID=sa;Password=123456";
                string connStr = "Data Source=localhost;Initial Catalog=PMS2024;Integrated Security=True;Trust Server Certificate=True";
                //string connStr = "server=localhost;Database=PMS2024;Uid=root;Pwd=123456;charset=utf8mb4;pooling=true";
                ConnectionConfig config = new ConnectionConfig()
                {
                    DbType = SqlSugar.DbType.SqlServer,
                    //DbType = SqlSugar.DbType.MySql,
                    ConnectionString = connStr,
                    IsAutoCloseConnection = true
                };

                return new SqlSugarClient(config);
            });
            services.AddTransient<IUserService, UserSerivce>();
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<IMenuService, MenuService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IBaseInfoService, BaseInfoService>();
            services.AddTransient<IOwnerService, OwnerService>();
            services.AddTransient<IFeeService, FeeService>();

        }

        private static void ConfigAuthentication(IServiceCollection service)
        {
            service
                .AddAuthentication(a =>
                {
                    a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(jwt =>
                {
                    jwt.RequireHttpsMetadata = false;
                    jwt.SaveToken = true;
                    jwt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("123456123456123456中华人民共和国")),
                        ValidIssuer = "webapi.cn",
                        ValidAudience = "WebApi",
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            // 配置Swgger的鉴权信息
            service.AddSwaggerGen(option =>
            {
                //添加安全定义--配置支持token授权机制
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "请输入token,格式为 Bearer xxxxxxxx（注意中间必须有空格）",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference =new OpenApiReference()
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id ="Bearer"
                            }
                        },
                        new string[]{ }
                    }
                });
            });


        }
    }
}

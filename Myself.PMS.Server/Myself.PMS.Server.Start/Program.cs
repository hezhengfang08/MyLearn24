
using Myself.PMS.Server.IService;
using Myself.PMS.Server.Service;
using SqlSugar;

namespace Myself.PMS.Server.Start
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            RegistarClasses(builder.Services);



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
        }
    }
}

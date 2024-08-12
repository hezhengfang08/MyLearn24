
using MySelf.PMS.Server.IService;
using MySelf.PMS.Server.Service;
using SqlSugar;


namespace Zhaoxi.PMS.Server.Start
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //// 测试   演示基本的SqlSugar的使用
            //string connStr = "Data Source=localhost;Initial Catalog=PMS2024;User ID=sa;Password=123456";
            //ConnectionConfig config = new ConnectionConfig()
            //{
            //    DbType = SqlSugar.DbType.SqlServer,
            //    ConnectionString = connStr,
            //    IsAutoCloseConnection = true
            //};

            //SqlSugarClient sqlSugarClient = new SqlSugarClient(config);

            ////client.Open();
            //var es = sqlSugarClient.Queryable<SysEmployee>().ToList();



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

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }


        private static void RegistarClasses(IServiceCollection services)
        {
            services.AddTransient<ISqlSugarClient>(client =>
            {
                //string connStr = "Data Source=localhost;Initial Catalog=PMS2024;User ID=sa;Password=123456";
                string connStr = "Data Source=localhost\\SQLEXPRESS01;Initial Catalog=PMS2024;Integrated Security=True;Trust Server Certificate=True";
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
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IFileService, FileService>();
        }
    }
}

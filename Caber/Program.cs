
using Caber.Contexts;
using Caber.Models;
using Caber.Repositories;
using Caber.Services;
using Microsoft.EntityFrameworkCore;

namespace Caber
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen();

            #region Contexts
            var DBHOST = Environment.GetEnvironmentVariable("DB_HOST");
            var DBPASS = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");
            var DBNAME = Environment.GetEnvironmentVariable("DB_NAME");
            var connectionString = @$"Server={DBHOST};Database={DBNAME};User Id=sa;Password={DBPASS};TrustServerCertificate=True";

            builder.Services.AddDbContext<CaberContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            #endregion

            #region Repositories
            builder.Services.AddScoped<IRepository<int, User>, UserRepository>();

            #endregion



            var app = builder.Build();

            DatabaseMigrationService.MigrateInitial(app);

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


    }
}

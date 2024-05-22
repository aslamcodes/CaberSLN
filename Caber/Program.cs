
using Caber.Contexts;
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
            ;
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


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            DatabaseMigrationService.MigrateInitial(app);

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

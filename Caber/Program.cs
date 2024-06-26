
using Caber.Contexts;
using Caber.Controllers;
using Caber.Models;
using Caber.Repositories;
using Caber.Repositories.Interfaces;
using Caber.Services;
using Caber.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.Json.Serialization;

namespace Caber
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddLogging(l => l.AddLog4Net());

            #region CORS
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                                       builder =>
                                       {
                                           builder.AllowAnyOrigin()
                                                  .AllowAnyMethod()
                                                  .AllowAnyHeader();
                                       });
            });
            #endregion

            #region SwaggerGen

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Test01", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."

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
                         new string[] {}
                    }
                });
            });
            #endregion

            #region Authentication
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenKey:Key"]))
                };
            });
            #endregion

            #region Contexts
            var DBHOST = Environment.GetEnvironmentVariable("DB_HOST");
            var DBPASS = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");
            var DBNAME = Environment.GetEnvironmentVariable("DB_NAME");
            var connectionString = @$"Server={DBHOST};Database={DBNAME};User Id=sa;Password={DBPASS};TrustServerCertificate=True";

            builder.Services.AddDbContext<CaberContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });


            builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            #endregion

            #region Repositories
            builder.Services.AddScoped<IRepository<int, User>, UserRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IRepository<int, Driver>, DriverRepository>();
            builder.Services.AddScoped<IRepository<int, Passenger>, PassengerRepository>();
            builder.Services.AddScoped<IRepository<int, Cab>, CabRepository>();
            builder.Services.AddScoped<IRepository<int, Ride>, RideRepository>();
            #endregion

            #region Services

            #region AuthServices
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IRoleService, RoleService>();
            #endregion

            #region BusinessServices
            builder.Services.AddScoped<IDriverService, DriverService>();
            builder.Services.AddScoped<IPassengerService, PassengerService>();
            builder.Services.AddScoped<ICabService, CabService>();
            builder.Services.AddScoped<IRideService, RideService>();
            builder.Services.AddScoped<IAdminService, AdminService>();
            builder.Services.AddScoped<IUserService, UserService>();
            #endregion

            #endregion

            #region Roles
            builder.Services.AddAuthorizationBuilder()
            .AddPolicy("Admin", policy => policy.RequireRole("Admin"))
            .AddPolicy("Driver", policy => policy.RequireRole("Driver"))
            .AddPolicy("Passenger", policy => policy.RequireRole("Passenger"));
            #endregion

            var app = builder.Build();

            DatabaseMigrationService.MigrateInitial(app);

            // Configure the HTTP request pipeline.

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}

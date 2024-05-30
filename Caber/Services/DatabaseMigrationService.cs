using Caber.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Caber.Services
{
    public class DatabaseMigrationService
    {

        [ExcludeFromCodeCoverage]
        public static void MigrateInitial(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();

            serviceScope.ServiceProvider.GetService<CaberContext>().Database
                                        .Migrate();
        }
    }
}

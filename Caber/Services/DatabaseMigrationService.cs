using Caber.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Caber.Services
{
    public class DatabaseMigrationService
    {
        public static void MigrateInitial(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();

            serviceScope.ServiceProvider.GetService<CaberContext>().Database
                                        .Migrate();
        }
    }
}

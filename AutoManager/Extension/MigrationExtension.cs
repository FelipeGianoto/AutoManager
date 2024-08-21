using AutoManager.Data;
using Microsoft.EntityFrameworkCore;

namespace AutoManager.Extension
{
    public static class MigrationExtension
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            using AutoManagerContext context = scope.ServiceProvider.GetRequiredService<AutoManagerContext>();

            context.Database.Migrate();
        }
    }
}

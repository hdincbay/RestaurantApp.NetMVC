using Microsoft.EntityFrameworkCore;
using Repositories;

namespace Restaurant.Infrastructe
{
    public static class ApplicationExtension
    {
        public static void ConfigureAndCheckMigration(this IApplicationBuilder app)
        {
            RepositoryContext context = app
                .ApplicationServices
                .CreateScope()
                .ServiceProvider
                .GetRequiredService<RepositoryContext>();
                if(context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }
        }
    }
}
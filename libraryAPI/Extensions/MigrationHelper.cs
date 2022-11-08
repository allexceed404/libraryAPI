using Microsoft.EntityFrameworkCore;
using libraryAPI.EfCore;
namespace libraryAPI.Extensions;
public static class MigrationHelper
{
    public static IApplicationBuilder MigrationDatabase(this IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            using (var context = scope.ServiceProvider.GetRequiredService<EF_DataContext>())
            {
                context.Database.Migrate();
            }
        }
        return app;
    }
}
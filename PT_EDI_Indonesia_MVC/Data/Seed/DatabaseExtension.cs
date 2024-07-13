using Microsoft.EntityFrameworkCore;
using PT_EDI_Indonesia_MVC.Data.Context;

namespace PT_EDI_Indonesia_MVC.Data.Seed;

public static class DatabaseExtension
{
    public static async Task ApplyMigration(this WebApplication app)
    {
        using var scope = app.Services.CreateAsyncScope();
        var service = scope.ServiceProvider.GetRequiredService<AccountContext>();
        var database = service.Database;
        await database.MigrateAsync();
    }
}
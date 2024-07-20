using Microsoft.EntityFrameworkCore;
using BiodataManagement.Data.Context;

namespace BiodataManagement.Data.Seed;

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
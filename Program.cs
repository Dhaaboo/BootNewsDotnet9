using BootNews.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ===== TALLAABADA 3: Database-ka =====
// Default: SQL Server (connection string-ka appsettings.json)
// Preview/Docker: DB_PROVIDER=sqlite => SQLite fudud (xogtu isku mid)
var provider = Environment.GetEnvironmentVariable("DB_PROVIDER") ?? "sqlserver";

builder.Services.AddDbContext<NewsDbContext>(options =>
{
    if (provider.Equals("sqlite", StringComparison.OrdinalIgnoreCase))
    {
        var dbPath = Environment.GetEnvironmentVariable("SQLITE_PATH") ?? "bootnews.db";
        options.UseSqlite($"Data Source={dbPath}");
    }
    else
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    }
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

// ===== Auto-migrate + seed marka app-ku furo =====
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<NewsDbContext>();
    if (provider.Equals("sqlite", StringComparison.OrdinalIgnoreCase))
        db.Database.EnsureCreated();   // sqlite: abuur schema-ka si toos ah
    else
        db.Database.Migrate();         // sqlserver: daji migrations-ka

    DbSeeder.Seed(db);                 // buuxi xogta tijaabada haddii madhan
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BiodataManagement.Authorization;
using BiodataManagement.Data.Context;
using BiodataManagement.Data.Identity;
using BiodataManagement.Data.Repository;
using BiodataManagement.Data.Seed;
using BiodataManagement.Service.Accounts;
using BiodataManagement.Service.Accounts.AccountService;
using BiodataManagement.Service.BiodataService;
using BiiodataManagement.Data.Scripts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// DbContext for dapper and EFCore
var connectionString = builder.Configuration.GetConnectionString("SQLConnection");
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddDbContext<AccountContext>(o =>
{
    o.UseSqlServer(connectionString);
});

// Asp.Net Core Identity for authentication and authorization
builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
{
    opt.Password.RequiredLength = 6;
    opt.User.RequireUniqueEmail = true;

}).AddEntityFrameworkStores<AccountContext>();

builder.Services.ConfigureApplicationCookie(o =>
{
    o.AccessDeniedPath = "/error/403";
});

builder.Services.AddAuthorization(configure =>
{
    configure.AddPolicy("BiodataOwner", policy =>
    {
        policy.AddRequirements(new BiodataOwnerOrAdminPolicy.BiodataOwnerOrAdminRequirement());
    });
});
builder.Services.AddScoped<IAuthorizationHandler, BiodataOwnerOrAdminPolicy.Handler>();


// Registering Entity Repository and Service
builder.Services.AddScoped<IBiodataRepository, BiodataRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<PendidikanTerakhirRepository>();

builder.Services.AddScoped<AccountService>();

builder.Services.AddScoped<GenerateData>();


var app = builder.Build();

// migration for Identity Table
await app.ApplyMigration();
await app.CreateRoles();
// migration for Entity Table and stored procedure
DbInitializer.Initialize(connectionString);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/error/{0}");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();



app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();

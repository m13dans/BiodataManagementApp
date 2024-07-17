using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PT_EDI_Indonesia_MVC.Authorization;
using PT_EDI_Indonesia_MVC.Data.Context;
using PT_EDI_Indonesia_MVC.Data.Identity;
using PT_EDI_Indonesia_MVC.Data.Repository;
using PT_EDI_Indonesia_MVC.Data.Seed;
using PT_EDI_Indonesia_MVC.Service.Accounts;
using PT_EDI_Indonesia_MVC.Service.Accounts.AccountService;
using PT_EDI_Indonesia_MVC.Service.BiodataService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// DbContext for dapper and EFCore
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddDbContext<AccountContext>(o =>
{
    o.UseSqlServer(builder.Configuration.GetConnectionString("SQLConnection"));
});

// Asp.Net Core Identity for authentication and authorization
builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
{
    opt.Password.RequiredLength = 6;
    opt.User.RequireUniqueEmail = true;

}).AddEntityFrameworkStores<AccountContext>();

// builder.Services.ConfigureApplicationCookie(o =>
// {
//     o.AccessDeniedPath = "/errors/404";
// });

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

await app.ApplyMigration();
await app.CreateRoles();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStatusCodePages("/error/{0}", "text/html");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();

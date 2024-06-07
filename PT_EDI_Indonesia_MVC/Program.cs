using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PT_EDI_Indonesia_MVC.Core.Models;
using PT_EDI_Indonesia_MVC.Data.Context;
using PT_EDI_Indonesia_MVC.Data.IRepository;
using PT_EDI_Indonesia_MVC.Data.Repository;
using PT_EDI_Indonesia_MVC.Data.Seed;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<DapperContext>();
builder.Services.AddDbContext<AccountContext>(o =>
{
    o.UseSqlServer(builder.Configuration.GetConnectionString("SQLConnection"));
});
builder.Services.AddIdentity<User, IdentityRole>(opt =>
{
    opt.Password.RequiredLength = 6;
    // opt.Password.RequireDigit = false;
    // opt.Password.RequireUppercase = false;
    // opt.Password.RequireNonAlphanumeric = false;

    opt.User.RequireUniqueEmail = true;


}).AddEntityFrameworkStores<AccountContext>();

// builder.Services.AddAuthorization();

builder.Services.AddScoped<IBiodataRepository, BiodataRepository>();
builder.Services.AddScoped<GenerateData>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

await app.CreateRoles();

app.Run();

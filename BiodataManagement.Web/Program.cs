using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BiodataManagement.Authorization;
using BiodataManagement.Data.Context;
using BiodataManagement.Data.Identity;
using BiodataManagement.Data.Repository;
using BiodataManagement.Data.Seed;
using BiodataManagement.Service.BiodataService;
using BiodataManagement.Data.Scripts;
using BiodataManagement.Web.Service.PendidikanTerakhirService;
using FluentValidation;
using BiodataManagement.Service.PendidikanTerakhirService;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using BiodataManagement.Domain.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// DbContext for dapper and EFCore
var connectionString = builder.Configuration.GetConnectionString("SQLConnection");
builder.Services.AddScoped<DbConnectionFactory>();
builder.Services.AddDbContext<AccountContext>(o =>
{
    o.UseSqlServer(connectionString);
});

// builder.Services.AddDateOnlyTimeOnlyStringConverters();
builder.Services.Configure<RequestLocalizationOptions>(o =>
{
    var supportedCulture = new[] { new CultureInfo("en-US"), new CultureInfo("id-Id") };
    o.DefaultRequestCulture = new RequestCulture("en-US");
    o.SupportedCultures = supportedCulture;
    o.SupportedUICultures = supportedCulture;
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

// Registering Validator Service
builder.Services.AddScoped<IValidator<BiodataCreateRequest>, BiodataCreateValidator>();
builder.Services.AddScoped<IValidator<Biodata>, BiodataUpdateValidator>();
builder.Services.AddScoped<IValidator<PendidikanTerakhirRequest>, PendidikanTerakhirCreateValidator>();
builder.Services.AddScoped<IValidator<PendidikanTerakhir>, PendidikanTerakhirUpdateValidator>();


// Registering Entity Repository and Service
builder.Services.AddScoped<IBiodataRepository, BiodataRepository>();
builder.Services.AddScoped<IPendidikanTerakhirRepository, PendidikanTerakhirRepository>();
builder.Services.AddScoped<PendidikanTerakhirService>();


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

using Entities.Models;
using log4net.Config;
using Microsoft.Extensions.DependencyInjection;
using Repositories;
using Restaurant.Infrastructe;
using Restaurant.Infrastructe.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache();
builder.Services.ConfigureSession();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.ConfigureRepositoryRegistration();
builder.Services.ConfigureServiceRegistration();

builder.Services.AddAutoMapper(typeof(Program));
var app = builder.Build();

app.ConfigureAndCheckMigration();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoint => {
    endpoint.MapAreaControllerRoute(
        name: "Admin",
        areaName: "Admin",
        pattern: "Admin/{controller=Home}/{action=Index}/{id?}"
    );
    
    endpoint.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
    );
    endpoint.MapRazorPages();
});

app.Run();

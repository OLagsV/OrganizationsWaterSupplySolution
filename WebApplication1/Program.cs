using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using OrganizationsWaterSupplyL4.Data;
using OrganizationsWaterSupplyL4.Middleware;
using OrganizationsWaterSupplyL4.Models;
using OrganizationsWaterSupplyL4.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
string conStr = builder.Configuration.GetConnectionString("SqlServerConnection");
builder.Services.AddDbContext<OrganizationsWaterSupplyContext>(options => options.UseSqlServer(conStr));
builder.Services.AddControllersWithViews();
builder.Services.AddMemoryCache();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddScoped<ICachedService<CounterModel>, CachedModelsService>();
builder.Services.AddScoped<ICachedService<Counter>, CachedCountersService>();
builder.Services.AddScoped<ICachedService<Organization>, CachedOrganizationsService>();
var app = builder.Build();
Debug.WriteLine("1234124");
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseDbInitializer();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "counters",
    pattern: "{controller=Counters}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "countermodels",
    pattern: "{controller=CounterModels}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "organizations",
    pattern: "{controller=Organizations}/{action=Index}/{id?}");

app.Run();

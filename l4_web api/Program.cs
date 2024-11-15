using l4_web_api.Data;
using l4_web_api.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
var builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
services.AddDbContext<MedicinalProductsContext>(options => options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddControllersWithViews();

// Добавление контекста базы данных
builder.Services.AddDbContext<MedicinalProductsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MedicinalProductsContext")));
builder.Services.AddDistributedMemoryCache(); // Для использования кэша
builder.Services.AddSession(); // Для использования сессий
builder.Services.AddControllers();

var app = builder.Build();



// Включение сессий
app.UseSession();

// Подключение middleware для инициализации базы данных
app.UseDbInitializer();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});


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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
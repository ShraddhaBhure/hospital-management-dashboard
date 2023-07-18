using HS.Data;
using HS.Models;
using HS.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddControllersWithViews(); var connectionString = builder.Configuration.GetConnectionString("DbConnection");
builder.Services.AddDbContextPool<MainContext>(option => option.UseSqlServer(connectionString));
builder.Services.AddScoped<NurseService>();
builder.Services.AddDbContext<MainContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
   pattern: "{controller=Login}/{action=LoginPage2}/{id?}");
 //pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

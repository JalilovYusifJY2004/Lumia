using LumiaPraktika.DAL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer
(builder.Configuration.GetConnectionString("Default")));
var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();



app.MapControllerRoute(
    "Default",
    "{area:exists}/{action=index}/{controller=home}/{id?}");
app.MapControllerRoute(
    "Default",
    "{action=index}/{controller=home}/{id?}");

app.Run();

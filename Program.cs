using Microsoft.EntityFrameworkCore;
using MyMvcApp.Data;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

DotNetEnv.Env.Load();
String connectionString=Environment.GetEnvironmentVariable("DB_URL") ?? throw new Exception("DB_URL is not set.");

// 註冊 Controller 與 View
builder.Services.AddControllersWithViews();

// 註冊 ApplicationDbContext 並使用 PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString)
);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
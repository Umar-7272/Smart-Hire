using Microsoft.EntityFrameworkCore;
using SmartHire.Data;
using SmartHire.DAL;
using SmartHire.BLL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<SmartHireContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("SmartHireDB")));

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpContextAccessor();

// DAL Register
builder.Services.AddScoped<UserDAL>();
builder.Services.AddScoped<JobDAL>();
builder.Services.AddScoped<ApplicationDAL>();
builder.Services.AddScoped<StudentProfileDAL>();
builder.Services.AddScoped<CompanyDAL>();

// BLL Register
builder.Services.AddScoped<UserBLL>();
builder.Services.AddScoped<JobBLL>();
builder.Services.AddScoped<ApplicationBLL>();
builder.Services.AddScoped<StudentProfileBLL>();
builder.Services.AddScoped<CompanyBLL>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
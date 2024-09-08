using Microsoft.AspNetCore.Authentication.Cookies;
using StockAnalysis.Web.Service;
using StockAnalysis.Web.Service.IService;
using StockAnalysis.Web.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.AddHttpClient<IAuthService, AuthService>();

SD.AuthAPIBase = builder.Configuration["ServiceUrls:AuthAPI"];
SD.DataAcquisition = builder.Configuration["ServiceUrls:DataAcquisition"];
SD.UserAnalytics = builder.Configuration["ServiceUrls:UserAnalytics"];


builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ISecurityService, SecurityService>();
builder.Services.AddScoped<ISecuritySyncService, SecuritySyncService>();
builder.Services.AddScoped<IPriceSyncService, PriceSyncService>();
builder.Services.AddScoped<IPriceService, PriceService>();
builder.Services.AddScoped<ISecurityRunService, SecurityRunService>();
builder.Services.AddScoped<IPriceRunService, PriceRunService>();
builder.Services.AddScoped<IPortfolioService, PortfolioService>();
builder.Services.AddScoped<IBankDepositService, BankDepositService>();
builder.Services.AddScoped<ICorporateActionService, CorporateActionService>();
builder.Services.AddScoped<ICorporateActionRunService, CorporateActionRunService>();
builder.Services.AddScoped<ICorporateActionTypeService, CorporateActionTypeService>();
builder.Services.AddScoped<ICorporateActionTypeRunService, CorporateActionTypeRunService>();

builder.Services.AddScoped<ITokenProvider, TokenProvider>();

builder.Services.AddScoped<IBaseService, BaseService>();


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromHours(10);
    options.LoginPath = "/Auth/Login";
    options.AccessDeniedPath = "/Auth/AccessDenied";
});
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

app.Run();

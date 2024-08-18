using UserAnalyticsService.Repository.IRepository;
using UserAnalyticsService.Repository;
using UserAnalyticsService.Service;
using UserAnalyticsService.Data;
using UserAnalyticsService;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using UserAnalyticsService.Utilities;
using UserAnalyticsService.Service.IService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure MongoDB globally for Guid as String
MongoDbSettings.ConfigureMongoDb();

// Register configuration settings
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));
builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));

SD.DataAcquisition = builder.Configuration["ServiceUrls:DataAcquisition"];
builder.Services.AddHttpClient();


// Register MongoDB Client and IMongoDatabase
builder.Services.AddSingleton<IMongoClient, MongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});

builder.Services.AddScoped(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return client.GetDatabase(settings.DatabaseName);
});

// Register MongoDB and repositories
builder.Services.AddSingleton<DBContext>();
builder.Services.AddScoped<IPortfolioRepository, PortfolioRepository>();
builder.Services.AddScoped<ISecuritySyncRepository, SecuritySyncRepository>();
builder.Services.AddScoped<ISecuritySyncRunRepository, SecuritySyncRunRepository>();
builder.Services.AddScoped<IPriceSyncRunRepository,PriceSyncRunRepository>();
builder.Services.AddScoped<IPriceSyncRepository,PriceSyncRepository>();
builder.Services.AddScoped<ISyncProcess, SyncProcess>();
builder.Services.AddScoped<PortfolioService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
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

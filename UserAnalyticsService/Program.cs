using UserAnalyticsService.Repository.IRepository;
using UserAnalyticsService.Repository;
using UserAnalyticsService.Service;
using UserAnalyticsService.Data;
using UserAnalyticsService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register MongoDB and repositories
builder.Services.AddSingleton<DBContext>(); // Register PortfolioContext as a singleton
builder.Services.AddScoped<IPortfolioRepository, PortfolioRepository>(); // Register PortfolioRepository for dependency injection
builder.Services.AddScoped<PortfolioService>();

builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));



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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

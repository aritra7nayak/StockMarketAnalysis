using DataAcquisitionService.Repository.IRepository;
using DataAcquisitionService.Repository;
using DataAcquisitionService.Data;
using Microsoft.EntityFrameworkCore;
using DataAcquisitionService.Services.IService;
using DataAcquisitionService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ISecurityService, SecurityService>();
builder.Services.AddScoped<ICorporateAnnouncementService, CorporateAnnouncementService>();
builder.Services.AddScoped<IUnitofWork,UnitOfWork>();
builder.Services.AddScoped<ISecurityRepository,SecurityRepository>();
builder.Services.AddScoped<ICorporateAnnouncementRepository,CorporateAnnouncementRepository>();
builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
}
);
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

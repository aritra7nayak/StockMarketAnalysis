using DataAcquisitionService.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAcquisitionService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Security> Securities { get; set; }
        public DbSet<SecurityRun> SecurityRun { get; set; }
        public DbSet<PriceRun> PriceRuns { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<CorporateActionTypeRun> CorporateActionTypeRuns { get; set; }
        public DbSet<CorporateActionType> CorporateActionTypes { get; set; }
        public DbSet<CorporateActionRun> CorporateActionRuns { get; set; }
        public DbSet<CorporateAction> CorporateActions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);



        }
    }
}

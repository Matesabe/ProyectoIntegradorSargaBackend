using BusinessLogic.Entities;
using Infrastructure.DataAccess.EF.Config;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess.EF
{
    public class SargaContext : DbContext
    {

        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<SubProduct> SubProducts { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<PurchasePromotionAmount> PurchasePromotionsAmount { get; set; }
        public DbSet<PurchasePromotionDate> PurchasePromotionsDate { get; set; }
        public DbSet<PurchasePromotionRecurrence> PurchasePromotionsRecurrence { get; set; }
        public DbSet<PurchasePromotionProducts> PurchasePromotionsProducts { get; set; }
        public DbSet<Redemption> Redemptions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"REMOVED");
            //base.OnConfiguring(optionsBuilder);
            //optionsBuilder.UseSqlServer(@"
            //            Data Source=(localdb)\MSSQLLocalDB;
            //           Initial Catalog=PruebaUsuario;   
            //           Integrated Security=True;");

        }
        
        public SargaContext() : base()
        {
        
        }

        public SargaContext(DbContextOptions<SargaContext> options) : 
            base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<WarehouseStock>()
                .HasKey(ws => new { ws.WarehouseId, ws.SubProductId });

            modelBuilder.Entity<WarehouseStock>()
                .HasOne(ws => ws.Warehouse)
                .WithMany(w => w.Stocks)
                .HasForeignKey(ws => ws.WarehouseId);

            modelBuilder.Entity<WarehouseStock>()
                .HasOne(ws => ws.SubProduct)
                .WithMany(sp => sp.Stocks)
                .HasForeignKey(ws => ws.SubProductId);
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}

using BusinessLogic.Entities;
using Infrastructure.DataAccess.EF.Config;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess.EF
{
    public class SargaContext : DbContext
    {
        // En SargaContext, cambia el DbSet<User> por DbSet<Seller>, DbSet<Client>, y/o DbSet<Administrator>
        // según las clases concretas que heredan de User.

        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Administrator> Administrators { get; set; }

        // Elimina o comenta la línea:
        // public DbSet<User> Users { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            //optionsBuilder.UseSqlServer(@"
            //            Data Source=(localdb)\MSSQLLocalDB;
            //            Initial Catalog=PruebaUsuario;   
            //            Integrated Security=True;");
        }

        public SargaContext(DbContextOptions<SargaContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}

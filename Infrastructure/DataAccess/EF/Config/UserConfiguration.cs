using BusinessLogic.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataAccess.EF.Config
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.OwnsOne(u => u.Name, n =>
            {
                n.Property(p => p.Value).HasColumnName("Name").IsRequired();
            });
            builder.OwnsOne(u => u.Password, p =>
            {
                p.Property(pp => pp.Value).HasColumnName("Password").IsRequired();
            });
            builder.OwnsOne(u => u.Email, e =>
            {
                e.Property(ep => ep.Value).HasColumnName("Email").IsRequired();
            });
            // Configura el resto de las propiedades normalmente
        }
    }
}

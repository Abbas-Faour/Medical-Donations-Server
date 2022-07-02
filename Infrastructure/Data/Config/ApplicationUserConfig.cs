using Core.Entites.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class ApplicationUserConfig : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasMany(m => m.Medicines)
                    .WithOne(u => u.ApplicationUser)
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.Address)
                    .WithOne(u => u.ApplicationUser)
                    .OnDelete(DeleteBehavior.Cascade);

            builder.Property(b => b.IsActive)
                    .HasDefaultValue(true);
        }
    }
}
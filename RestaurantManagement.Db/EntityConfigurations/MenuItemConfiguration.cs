using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Db.Models;

namespace RestaurantManagement.Db.EntityConfigurations;

public class MenuItemConfiguration : IEntityTypeConfiguration<MenuItem>
{
    public void Configure(EntityTypeBuilder<MenuItem> builder)
    {
        builder.HasKey(mi => mi.Id);

        builder.Property(mi => mi.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(mi => mi.Description)
            .HasMaxLength(500);

        builder.Property(mi => mi.Price)
            .IsRequired();

        builder.Property(mi => mi.Category)
            .HasMaxLength(50);
    }
}

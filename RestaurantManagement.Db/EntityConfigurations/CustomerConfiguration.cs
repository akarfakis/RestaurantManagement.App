using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Db.Models;

namespace RestaurantManagement.Db.EntityConfigurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(ds => ds.Id);

        builder.Property(ds => ds.FirstName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(ds => ds.LastName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(ds => ds.Phone)
            .HasMaxLength(20);

        builder.Property(ds => ds.Email)
            .HasMaxLength(100);

        builder.HasMany(c => c.Orders) 
            .WithOne(o => o.Customer)
            .HasForeignKey(o => o.CustomerId) 
            .OnDelete(DeleteBehavior.Cascade);
    }
}

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Db.Models;

namespace RestaurantManagement.Db.EntityConfigurations;
public class DeliveryStaffConfiguration : IEntityTypeConfiguration<DeliveryStaff>
{
    public void Configure(EntityTypeBuilder<DeliveryStaff> builder)
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

        // Relationships
        builder.HasMany(ds => ds.AssignedOrders)
            .WithOne(o => o.AssignedDeliveryStaff) 
            .HasForeignKey(o => o.AssignedDeliveryStaffId) 
            .OnDelete(DeleteBehavior.SetNull);
    }
}


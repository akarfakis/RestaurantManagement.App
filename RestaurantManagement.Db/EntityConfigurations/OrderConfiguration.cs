using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Db.Models;

namespace RestaurantManagement.Db.EntityConfigurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(o => o.OrderDate)
            .IsRequired();

        builder.Property(o => o.OrderType)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(o => o.Status)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(o => o.DeliveryAddress)
            .HasMaxLength(200);

        builder.HasOne(o => o.Customer)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.CustomerId);

        builder.HasMany(o => o.OrderItems)
            .WithOne(oi => oi.Order)
            .HasForeignKey(oi => oi.OrderId);

        builder.HasOne(o => o.AssignedDeliveryStaff)
            .WithMany(s => s.AssignedOrders)
            .HasForeignKey(o => o.AssignedDeliveryStaffId)
            .OnDelete(DeleteBehavior.SetNull); 
    }
}

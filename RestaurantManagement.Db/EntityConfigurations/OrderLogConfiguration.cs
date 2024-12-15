using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Db.Models;

namespace RestaurantManagement.Db.EntityConfigurations;

public class OrderLogConfiguration : IEntityTypeConfiguration<OrderLog>
{
    public void Configure(EntityTypeBuilder<OrderLog> builder)
    {
        builder.Property(ol => ol.Timestamp)
            .IsRequired();

        builder.Property(ol => ol.Status)
            .IsRequired()
            .HasConversion<string>();

        builder.HasOne(ol => ol.Order)
            .WithMany(o => o.OrderLogs)
            .HasForeignKey(ol => ol.OrderId)
            .OnDelete(DeleteBehavior.Cascade); 
    }
}

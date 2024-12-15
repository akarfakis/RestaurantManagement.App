using System.ComponentModel;

namespace RestaurantManagement.Db.Enums;

public enum OrderStatus
{
    [Description("Pending")]
    Pending,

    [Description("Being prepared")]
    Preparing,

    [Description("Ready for pickup")]
    ReadyForPickup,

    [Description("Ready for delivery")]
    ReadyForDelivery,

    [Description("Out for delivery")]
    OutForDelivery,

    [Description("Delivered")]
    Delivered,

    [Description("Cancelled")]
    Cancelled,

    [Description("Unable To Deliver")]
    UnableToDeliver,
}
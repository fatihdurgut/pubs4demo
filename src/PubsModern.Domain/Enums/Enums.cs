namespace PubsModern.Domain.Enums;

public enum ContractStatus
{
    Pending = 0,
    Active = 1,
    Suspended = 2,
    Terminated = 3
}

public enum BookType
{
    Business = 0,
    Psychology = 1,
    Technology = 2,
    Cooking = 3,
    Traditional = 4,
    Popular = 5,
    ModernCooking = 6
}

public enum OrderStatus
{
    Pending = 0,
    Confirmed = 1,
    Shipped = 2,
    Delivered = 3,
    Cancelled = 4
}

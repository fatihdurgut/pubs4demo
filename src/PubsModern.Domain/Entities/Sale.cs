namespace PubsModern.Domain.Entities;

using PubsModern.Domain.Common;
using PubsModern.Domain.Enums;

/// <summary>
/// Sale/Order aggregate root
/// </summary>
public class Sale : BaseEntity
{
    public string OrderNumber { get; private set; }
    public Guid StoreId { get; private set; }
    public Store? Store { get; private set; }
    public DateTime OrderDate { get; private set; }
    public OrderStatus Status { get; private set; }
    public string? Notes { get; private set; }

    private readonly List<SaleItem> _items = new();
    public IReadOnlyCollection<SaleItem> Items => _items.AsReadOnly();

    // EF Core constructor
    private Sale()
    {
        OrderNumber = string.Empty;
    }

    public Sale(string orderNumber, Guid storeId)
    {
        if (string.IsNullOrWhiteSpace(orderNumber))
            throw new ArgumentException("Order number cannot be empty", nameof(orderNumber));

        OrderNumber = orderNumber;
        StoreId = storeId;
        OrderDate = DateTime.UtcNow;
        Status = OrderStatus.Pending;
    }

    public void AddItem(Book book, int quantity, decimal discount = 0)
    {
        if (book == null)
            throw new ArgumentNullException(nameof(book));
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be positive", nameof(quantity));

        var item = new SaleItem(Id, book.Id, quantity, book.Price.Amount, discount);
        _items.Add(item);
    }

    public void UpdateStatus(OrderStatus status)
    {
        Status = status;
    }

    public void AddNotes(string notes)
    {
        Notes = notes;
    }

    public decimal GetTotalAmount()
    {
        return _items.Sum(item => item.GetLineTotal());
    }
}

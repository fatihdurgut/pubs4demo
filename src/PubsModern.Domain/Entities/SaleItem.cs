namespace PubsModern.Domain.Entities;

using PubsModern.Domain.Common;

/// <summary>
/// Sale line item entity
/// </summary>
public class SaleItem : BaseEntity
{
    public Guid SaleId { get; private set; }
    public Sale? Sale { get; private set; }
    
    public Guid BookId { get; private set; }
    public Book? Book { get; private set; }
    
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal Discount { get; private set; }

    // EF Core constructor
    private SaleItem() { }

    public SaleItem(Guid saleId, Guid bookId, int quantity, decimal unitPrice, decimal discount = 0)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be positive", nameof(quantity));
        if (unitPrice < 0)
            throw new ArgumentException("Unit price cannot be negative", nameof(unitPrice));
        if (discount < 0 || discount > 100)
            throw new ArgumentException("Discount must be between 0 and 100", nameof(discount));

        SaleId = saleId;
        BookId = bookId;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Discount = discount;
    }

    public decimal GetLineTotal()
    {
        var subtotal = Quantity * UnitPrice;
        var discountAmount = subtotal * (Discount / 100);
        return subtotal - discountAmount;
    }

    public void UpdateQuantity(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be positive", nameof(quantity));

        Quantity = quantity;
    }

    public void UpdateDiscount(decimal discount)
    {
        if (discount < 0 || discount > 100)
            throw new ArgumentException("Discount must be between 0 and 100", nameof(discount));

        Discount = discount;
    }
}

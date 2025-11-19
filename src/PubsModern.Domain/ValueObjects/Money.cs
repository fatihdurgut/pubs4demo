namespace PubsModern.Domain.ValueObjects;

using PubsModern.Domain.Common;

/// <summary>
/// Money value object representing currency amounts
/// </summary>
public class Money : ValueObject
{
    public decimal Amount { get; private set; }
    public string Currency { get; private set; }

    private Money() 
    {
        Currency = "USD";
    }

    public Money(decimal amount, string currency = "USD")
    {
        if (amount < 0)
            throw new ArgumentException("Amount cannot be negative", nameof(amount));
        
        Amount = amount;
        Currency = currency ?? throw new ArgumentNullException(nameof(currency));
    }

    public static Money operator +(Money left, Money right)
    {
        if (left.Currency != right.Currency)
            throw new InvalidOperationException("Cannot add money with different currencies");
        
        return new Money(left.Amount + right.Amount, left.Currency);
    }

    public static Money operator -(Money left, Money right)
    {
        if (left.Currency != right.Currency)
            throw new InvalidOperationException("Cannot subtract money with different currencies");
        
        return new Money(left.Amount - right.Amount, left.Currency);
    }

    public static Money operator *(Money money, decimal multiplier)
    {
        return new Money(money.Amount * multiplier, money.Currency);
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }

    public override string ToString()
    {
        return $"{Amount:C} {Currency}";
    }
}

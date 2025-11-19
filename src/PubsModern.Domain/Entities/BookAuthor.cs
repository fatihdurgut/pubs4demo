namespace PubsModern.Domain.Entities;

using PubsModern.Domain.Common;

/// <summary>
/// Many-to-many relationship between Books and Authors
/// </summary>
public class BookAuthor : BaseEntity
{
    public Guid BookId { get; private set; }
    public Book? Book { get; private set; }
    
    public Guid AuthorId { get; private set; }
    public Author? Author { get; private set; }
    
    public int AuthorOrder { get; private set; }
    public decimal RoyaltyPercentage { get; private set; }

    // EF Core constructor
    private BookAuthor() { }

    public BookAuthor(Guid bookId, Guid authorId, int authorOrder, decimal royaltyPercentage)
    {
        if (royaltyPercentage < 0 || royaltyPercentage > 100)
            throw new ArgumentException("Royalty percentage must be between 0 and 100", nameof(royaltyPercentage));

        BookId = bookId;
        AuthorId = authorId;
        AuthorOrder = authorOrder;
        RoyaltyPercentage = royaltyPercentage;
    }

    public void UpdateRoyaltyPercentage(decimal percentage)
    {
        if (percentage < 0 || percentage > 100)
            throw new ArgumentException("Royalty percentage must be between 0 and 100", nameof(percentage));

        RoyaltyPercentage = percentage;
    }

    public void UpdateAuthorOrder(int order)
    {
        AuthorOrder = order;
    }
}

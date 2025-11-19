namespace PubsModern.Domain.Entities;

using PubsModern.Domain.Common;
using PubsModern.Domain.ValueObjects;
using PubsModern.Domain.Enums;

/// <summary>
/// Book aggregate root
/// </summary>
public class Book : BaseEntity
{
    public ISBN ISBN { get; private set; }
    public string Title { get; private set; }
    public BookType Type { get; private set; }
    public Guid PublisherId { get; private set; }
    public Publisher? Publisher { get; private set; }
    public Money Price { get; private set; }
    public DateTime? PublishedDate { get; private set; }
    public string? Description { get; private set; }
    public string? CoverImageUrl { get; private set; }
    public int YearToDateSales { get; private set; }

    private readonly List<BookAuthor> _bookAuthors = new();
    public IReadOnlyCollection<BookAuthor> BookAuthors => _bookAuthors.AsReadOnly();

    private readonly List<SaleItem> _saleItems = new();
    public IReadOnlyCollection<SaleItem> SaleItems => _saleItems.AsReadOnly();

    // EF Core constructor
    private Book()
    {
        ISBN = null!;
        Title = string.Empty;
        Price = null!;
    }

    public Book(ISBN isbn, string title, BookType type, Guid publisherId, Money price)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty", nameof(title));

        ISBN = isbn ?? throw new ArgumentNullException(nameof(isbn));
        Title = title;
        Type = type;
        PublisherId = publisherId;
        Price = price ?? throw new ArgumentNullException(nameof(price));
        YearToDateSales = 0;
    }

    public void UpdateDetails(string title, BookType type, string? description)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty", nameof(title));

        Title = title;
        Type = type;
        Description = description;
    }

    public void UpdatePrice(Money price)
    {
        Price = price ?? throw new ArgumentNullException(nameof(price));
    }

    public void UpdateCoverImage(string imageUrl)
    {
        CoverImageUrl = imageUrl;
    }

    public void Publish(DateTime publishDate)
    {
        PublishedDate = publishDate;
    }

    public void RecordSale(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be positive", nameof(quantity));

        YearToDateSales += quantity;
    }

    public void AddAuthor(Author author, int authorOrder, decimal royaltyPercentage)
    {
        if (author == null)
            throw new ArgumentNullException(nameof(author));

        var bookAuthor = new BookAuthor(Id, author.Id, authorOrder, royaltyPercentage);
        _bookAuthors.Add(bookAuthor);
    }
}

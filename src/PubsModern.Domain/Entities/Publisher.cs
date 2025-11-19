namespace PubsModern.Domain.Entities;

using PubsModern.Domain.Common;
using PubsModern.Domain.ValueObjects;

/// <summary>
/// Publisher aggregate root
/// </summary>
public class Publisher : BaseEntity
{
    public string Name { get; private set; }
    public Address? Address { get; private set; }
    public string? Phone { get; private set; }
    public string? Email { get; private set; }
    public string? Website { get; private set; }

    private readonly List<Book> _books = new();
    public IReadOnlyCollection<Book> Books => _books.AsReadOnly();

    // EF Core constructor
    private Publisher()
    {
        Name = string.Empty;
    }

    public Publisher(string name, string? email = null, string? phone = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Publisher name cannot be empty", nameof(name));

        Name = name;
        Email = email;
        Phone = phone;
    }

    public void UpdateContactInfo(string? email, string? phone, string? website)
    {
        Email = email;
        Phone = phone;
        Website = website;
    }

    public void UpdateAddress(Address address)
    {
        Address = address ?? throw new ArgumentNullException(nameof(address));
    }

    public void AddBook(Book book)
    {
        if (book == null)
            throw new ArgumentNullException(nameof(book));
        
        _books.Add(book);
    }
}

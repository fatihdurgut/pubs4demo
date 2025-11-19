namespace PubsModern.Domain.Entities;

using PubsModern.Domain.Common;
using PubsModern.Domain.ValueObjects;
using PubsModern.Domain.Enums;

/// <summary>
/// Author aggregate root
/// </summary>
public class Author : BaseEntity
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string? Phone { get; private set; }
    public Address? Address { get; private set; }
    public string? Biography { get; private set; }
    public ContractStatus ContractStatus { get; private set; }

    private readonly List<BookAuthor> _bookAuthors = new();
    public IReadOnlyCollection<BookAuthor> BookAuthors => _bookAuthors.AsReadOnly();

    // EF Core constructor
    private Author()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        Email = string.Empty;
    }

    public Author(string firstName, string lastName, string email, string? phone = null)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name cannot be empty", nameof(firstName));
        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name cannot be empty", nameof(lastName));
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty", nameof(email));

        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Phone = phone;
        ContractStatus = ContractStatus.Pending;
    }

    public string FullName => $"{FirstName} {LastName}";

    public void UpdateContactInfo(string email, string? phone)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty", nameof(email));

        Email = email;
        Phone = phone;
    }

    public void UpdateAddress(Address address)
    {
        Address = address ?? throw new ArgumentNullException(nameof(address));
    }

    public void UpdateBiography(string biography)
    {
        Biography = biography;
    }

    public void UpdateContractStatus(ContractStatus status)
    {
        ContractStatus = status;
    }

    public void AddBook(Book book, int authorOrder, decimal royaltyPercentage)
    {
        if (book == null)
            throw new ArgumentNullException(nameof(book));

        var bookAuthor = new BookAuthor(book.Id, Id, authorOrder, royaltyPercentage);
        _bookAuthors.Add(bookAuthor);
    }
}

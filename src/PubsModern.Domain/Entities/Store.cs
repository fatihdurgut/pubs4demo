namespace PubsModern.Domain.Entities;

using PubsModern.Domain.Common;
using PubsModern.Domain.ValueObjects;

/// <summary>
/// Store aggregate root
/// </summary>
public class Store : BaseEntity
{
    public string Name { get; private set; }
    public Address? Address { get; private set; }
    public string? Phone { get; private set; }
    public string? Email { get; private set; }

    private readonly List<Sale> _sales = new();
    public IReadOnlyCollection<Sale> Sales => _sales.AsReadOnly();

    // EF Core constructor
    private Store()
    {
        Name = string.Empty;
    }

    public Store(string name, string? phone = null, string? email = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Store name cannot be empty", nameof(name));

        Name = name;
        Phone = phone;
        Email = email;
    }

    public void UpdateContactInfo(string? phone, string? email)
    {
        Phone = phone;
        Email = email;
    }

    public void UpdateAddress(Address address)
    {
        Address = address ?? throw new ArgumentNullException(nameof(address));
    }
}

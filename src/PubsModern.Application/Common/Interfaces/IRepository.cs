namespace PubsModern.Application.Common.Interfaces;

using PubsModern.Domain.Entities;

/// <summary>
/// Generic repository interface for data access
/// </summary>
public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
}

/// <summary>
/// Author repository interface with specific operations
/// </summary>
public interface IAuthorRepository : IRepository<Author>
{
    Task<IEnumerable<Author>> GetAuthorsWithBooksAsync(CancellationToken cancellationToken = default);
    Task<Author?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
}

/// <summary>
/// Book repository interface with specific operations
/// </summary>
public interface IBookRepository : IRepository<Book>
{
    Task<IEnumerable<Book>> GetBooksWithAuthorsAsync(CancellationToken cancellationToken = default);
    Task<Book?> GetByISBNAsync(string isbn, CancellationToken cancellationToken = default);
    Task<IEnumerable<Book>> SearchBooksAsync(string searchTerm, CancellationToken cancellationToken = default);
}

/// <summary>
/// Publisher repository interface
/// </summary>
public interface IPublisherRepository : IRepository<Publisher>
{
    Task<IEnumerable<Publisher>> GetPublishersWithBooksAsync(CancellationToken cancellationToken = default);
}

/// <summary>
/// Store repository interface
/// </summary>
public interface IStoreRepository : IRepository<Store>
{
    Task<IEnumerable<Store>> GetStoresWithSalesAsync(CancellationToken cancellationToken = default);
}

/// <summary>
/// Sale repository interface
/// </summary>
public interface ISaleRepository : IRepository<Sale>
{
    Task<IEnumerable<Sale>> GetSalesWithItemsAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Sale>> GetSalesByStoreAsync(Guid storeId, CancellationToken cancellationToken = default);
}

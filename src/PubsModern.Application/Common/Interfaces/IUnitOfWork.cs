namespace PubsModern.Application.Common.Interfaces;

/// <summary>
/// Unit of Work pattern interface for managing transactions
/// </summary>
public interface IUnitOfWork : IDisposable
{
    IAuthorRepository Authors { get; }
    IBookRepository Books { get; }
    IPublisherRepository Publishers { get; }
    IStoreRepository Stores { get; }
    ISaleRepository Sales { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}

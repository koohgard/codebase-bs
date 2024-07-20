
using Microsoft.EntityFrameworkCore.Storage;
namespace Infrastructure.Context;
public interface ITransactionManager
{
	IDbContextTransaction Transaction { get; }
	Task BeginTransactionAsync(CancellationToken cancellationToken = default);
	Task CommitTransactionAsync(CancellationToken cancellationToken = default);
	Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}
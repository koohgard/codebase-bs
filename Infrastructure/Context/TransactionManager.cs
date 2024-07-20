
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
namespace Infrastructure.Context;
public class TransactionManager : ITransactionManager
{
	private readonly DbContext appDbContext;
	public IDbContextTransaction Transaction { get; private set; }

	public TransactionManager(AppDbContext appDbContext)
	{
		this.appDbContext = appDbContext;
	}

	public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
	{
		Transaction = await appDbContext.Database.BeginTransactionAsync(cancellationToken: cancellationToken);
	}

	public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
	{
		await appDbContext.SaveChangesAsync(cancellationToken: cancellationToken);
		await Transaction.CommitAsync(cancellationToken: cancellationToken);
	}

	public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
	{
		await Transaction.RollbackAsync(cancellationToken: cancellationToken);
	}
}
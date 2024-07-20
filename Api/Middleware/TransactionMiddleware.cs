using Infrastructure.Context;
namespace Api.Middleware;
public class TransactionMiddleware
{
	private readonly RequestDelegate next;
	public TransactionMiddleware(RequestDelegate next)
	{
		this.next = next;
	}

	public async Task InvokeAsync(HttpContext context, ITransactionManager transactionManager)
	{
		var cancellationToken = context?.RequestAborted ?? CancellationToken.None;
		await transactionManager.BeginTransactionAsync(cancellationToken);
		try
		{
			await next(context);
			await transactionManager.CommitTransactionAsync(cancellationToken);
		}
		catch
		{
			await transactionManager.RollbackTransactionAsync(cancellationToken);
			throw;
		}
	}
}
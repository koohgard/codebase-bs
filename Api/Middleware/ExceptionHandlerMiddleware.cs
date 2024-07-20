namespace Api.Middleware;
public class ExceptionHandlerMiddleware
{
	private readonly RequestDelegate next;
	private readonly ILogger<ExceptionHandlerMiddleware> logger;

	public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
	{
		this.next = next;
		this.logger = logger;
	}

	public async Task InvokeAsync(HttpContext context)
	{
		try
		{
			await next(context);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, ex.Message);

			context.Response.Clear();
			context.Response.StatusCode = StatusCodes.Status500InternalServerError;
			context.Response.ContentType = "text/plain";
			await context.Response.WriteAsync(ex.Message);
		}
	}
}
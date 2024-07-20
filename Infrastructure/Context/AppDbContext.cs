using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Infrastructure.MapConfig;

namespace Infrastructure.Context;
public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{
	}
	public DbSet<User> Users { get; set; }
	public DbSet<Book> Books { get; set; }
	public DbSet<Order> Orders { get; set; }
	public DbSet<OrderDetail> OrderDetails { get; set; }
	public DbSet<StockTransaction> StockTransactions { get; set; }
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		base.OnConfiguring(optionsBuilder);
	}
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.ApplyConfiguration(new UserMapConfig());
		modelBuilder.ApplyConfiguration(new BookMapConfig());
		modelBuilder.ApplyConfiguration(new OrderMapConfig());
		modelBuilder.ApplyConfiguration(new OrderDetailMapConfig());
		modelBuilder.ApplyConfiguration(new StockTransactionMapConfig());
	}
}
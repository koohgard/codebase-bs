using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Infrastructure.MapConfig;
public class OrderDetailMapConfig : IEntityTypeConfiguration<OrderDetail>
{
	public void Configure(EntityTypeBuilder<OrderDetail> builder)
	{
		builder.HasKey(x => x.Id);
		builder.Property(x => x.Id).ValueGeneratedOnAdd();
		builder.Property(x => x.OrderId).IsRequired();
		builder.Property(x => x.BookId).IsRequired();
		builder.Property(x => x.StockTransactionId).IsRequired();
		builder.Property(x => x.Count).IsRequired();

		builder.HasOne(x => x.Order)
			   .WithMany(x => x.OrderDetails)
			   .HasForeignKey(x => x.OrderId);

		builder.HasOne(x => x.Book)
			   .WithMany(x => x.OrderDetails)
			   .HasForeignKey(x => x.BookId);

		builder.HasOne(x => x.StockTransaction)
			   .WithOne(x => x.OrderDetail)
			   .HasForeignKey<OrderDetail>(x => x.StockTransactionId);


	}
}
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Infrastructure.MapConfig;
public class StockTransactionMapConfig : IEntityTypeConfiguration<StockTransaction>
{
	public void Configure(EntityTypeBuilder<StockTransaction> builder)
	{
		builder.HasKey(x => x.Id);
		builder.Property(x => x.Id).ValueGeneratedOnAdd();
		builder.Property(x => x.BookId).IsRequired();
		builder.Property(x => x.Count).IsRequired();
		builder.Property(x => x.TransactionType).IsRequired();
		builder.Property(x => x.TransactionFactor).IsRequired();
		builder.Property(x => x.CreateDateTime).IsRequired();

		builder.HasOne(x => x.Book)
			   .WithMany(x => x.StockTransactions)
			   .HasForeignKey(x => x.BookId);
	}
}
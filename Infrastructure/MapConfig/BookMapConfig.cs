using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Infrastructure.MapConfig;
public class BookMapConfig : IEntityTypeConfiguration<Book>
{
	public void Configure(EntityTypeBuilder<Book> builder)
	{

		builder.HasKey(x => x.Id);
		builder.Property(x => x.Id).ValueGeneratedOnAdd();
		builder.Property(x => x.Title).IsRequired().HasMaxLength(500);
		builder.Property(x => x.Description).HasMaxLength(1000);
		builder.Property(x => x.Price).IsRequired();
		builder.Property(x => x.IsDeleted).IsRequired();
		builder.Property(x => x.OutOfStock).IsRequired();
		builder.Property(x => x.RowVersion).IsRowVersion();
	}
}
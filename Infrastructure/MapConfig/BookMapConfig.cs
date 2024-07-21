using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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
		builder.Property(x => x.RowVersion).HasColumnName("xmin")
			.HasColumnType("xid")
			.IsRowVersion()
			.IsRequired()
			.HasConversion(
				new ValueConverter<byte[], uint>(
					convertToProviderExpression: v => BitConverter.ToUInt32(v, 0),
					convertFromProviderExpression: v => BitConverter.GetBytes(v)
				)
			);
	}
}
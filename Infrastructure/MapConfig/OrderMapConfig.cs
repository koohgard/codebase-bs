using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Infrastructure.MapConfig;
public class OrderMapConfig : IEntityTypeConfiguration<Order>
{
	public void Configure(EntityTypeBuilder<Order> builder)
	{

		builder.HasKey(x => x.Id);
		builder.Property(x => x.Id).ValueGeneratedOnAdd();
		builder.Property(x => x.UserId).IsRequired();
		builder.Property(x => x.CreatedDateTime).IsRequired();
		builder.Property(x => x.OrderStatus).IsRequired();

		builder.HasOne(x => x.User)
			   .WithMany(x => x.Orders)
			   .HasForeignKey(x => x.UserId);
	}
}
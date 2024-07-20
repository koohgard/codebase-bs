using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Infrastructure.MapConfig;
public class UserMapConfig : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.HasKey(x => x.Id);
		builder.Property(x => x.Id).ValueGeneratedOnAdd();
		builder.Property(x => x.Email).HasMaxLength(255).IsRequired();
		builder.Property(x => x.Password).HasMaxLength(255).IsRequired();
		builder.Property(x => x.UserType).IsRequired();
		builder.Property(x => x.LoginFaildCount).IsRequired();
		builder.Property(x => x.LockoutDateTime).IsRequired();
	}
}
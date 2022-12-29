using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ClientConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.HasMany(u => u.Groups)
			.WithMany(g => g.Users);

		builder.HasMany(u => u.Messages)
			.WithOne(m => m.User);
	}	
}
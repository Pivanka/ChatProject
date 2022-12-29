using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configurations
{
	public class MessageConfiguration : IEntityTypeConfiguration<Message>
	{
		public void Configure(EntityTypeBuilder<Message> builder)
		{
			builder.HasKey(e => e.Id);
			builder.Property(e => e.Text)
				.IsRequired();
			builder.Property(e => e.UserId)
				.IsRequired();
			builder.Property(e => e.GroupId)
				.IsRequired();
		}
	}
}

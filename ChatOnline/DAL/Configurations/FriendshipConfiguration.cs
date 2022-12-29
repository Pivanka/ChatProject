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
    internal class FriendshipConfiguration : IEntityTypeConfiguration<Friendship>
	{
		public void Configure(EntityTypeBuilder<Friendship> builder)
		{
            builder.HasKey(t => new { t.UserId, t.FriendId });
		}
	}
}

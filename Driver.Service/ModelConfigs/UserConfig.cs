using Driver.Service.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver.Service.ModelConfigs
{
	public class UserConfig : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.ToTable("Users");
			builder.HasKey(x => x.Id);
			builder.HasIndex(e => e.PhoneNumber).IsUnique();
			builder.Property(e => e.PhoneNumber).HasMaxLength(15).IsRequired();
			builder.Property(e => e.PasswordHash).HasMaxLength(100).IsRequired();
			builder.Property(e => e.PasswordSalt).HasMaxLength(100).IsRequired();
			builder.HasQueryFilter(e => !e.IsDeleted);
		}
	}
}

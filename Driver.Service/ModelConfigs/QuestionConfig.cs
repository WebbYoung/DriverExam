using Driver.Service.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Driver.Service.ModelConfigs
{
	public class QuestionConfig : IEntityTypeConfiguration<Question>
	{
		public void Configure(EntityTypeBuilder<Question> builder)
		{
			builder.ToTable("Questions");
			builder.HasKey(x => x.Id);
			builder.Property(e => e.Title).HasMaxLength(200).IsRequired();
			builder.OwnsMany(e => e.Options, e =>
			{
				e.Property<int>("OptionsId")
					.ValueGeneratedOnAdd() 
					.HasColumnType("INTEGER")
					.HasAnnotation("Sqlite:Autoincrement", true);
				e.HasKey("OptionsId");
				e.Property(e => e.OptionChar).IsRequired();
				e.Property(e=>e.Content).HasMaxLength(100).IsRequired();
				e.Property(e => e.IsAnswer).IsRequired();
			});
			builder.HasQueryFilter(e => !e.IsDeleted);
		}
	}
}

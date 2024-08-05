using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PocketMate.Core.Models;

namespace PocketMate.Api.Data.Mappings
{
	public class CategoryMapping : IEntityTypeConfiguration<Category>
	{
		public void Configure(EntityTypeBuilder<Category> builder)
		{
			builder.ToTable("Category");

			builder.HasKey(x => x.Id);

			builder.Property(x => x.Title)
				.HasMaxLength(80)
				.HasColumnType("VARCHAR")
				.IsRequired(true);
			builder.Property(x => x.Description)
				.IsRequired(false)
				.HasColumnType("VARCHAR")
				.HasMaxLength(255);
			builder.Property(x => x.UserId)
				.HasMaxLength(160)
				.HasColumnType("VARCHAR")
				.IsRequired(true);
		}
	}
}

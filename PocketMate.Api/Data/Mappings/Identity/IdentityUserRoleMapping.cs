﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace PocketMate.Api.Data.Mappings.Identity
{
	public class IdentityUserRoleMapping : IEntityTypeConfiguration<IdentityUserRole<long>>
	{
		public void Configure(EntityTypeBuilder<IdentityUserRole<long>> builder)
		{
			builder.ToTable("IdentityUserRole");
			builder.HasKey(r => new { r.UserId, r.RoleId });
		}
	}
}
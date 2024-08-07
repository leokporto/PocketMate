﻿using Microsoft.EntityFrameworkCore;
using PocketMate.Core.Models;
using System.Reflection;

namespace PocketMate.Api.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}

		public DbSet<Category> Categories { get; set; } = null!;

		public DbSet<Transaction> Transactions { get; set; } = null!;

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}
	}
}

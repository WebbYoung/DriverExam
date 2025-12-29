using Driver.Service.Entities;
using Driver.Service.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Driver.Service
{
	public class DriverDbContext : DbContext
	{
		public DbSet<Question> Questions { get; set; }
		public DbSet<User> Users { get; set; }
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
			//这边我尝试了Environment.CurrentDirectory和Directory.GetCurrentDirectory但全部木大，问豆包说这个对
			string path = Path.Combine(AppContext.BaseDirectory, "Driver.db");
			string connectionString = $"Data Source={path};Cache=Shared";
			optionsBuilder.UseSqlite(connectionString)
				.LogTo(Console.WriteLine);
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}
	}
}

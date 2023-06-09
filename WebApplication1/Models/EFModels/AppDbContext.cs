using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace WebApplication1.Models.EFModels
{
	public partial class AppDbContext : DbContext
	{
		public AppDbContext()
			: base("name=AppDbContext")
		{
		}

		public virtual DbSet<Category> Categories { get; set; }
		public virtual DbSet<GuestBook> GuestBooks { get; set; }
		public virtual DbSet<News> News { get; set; }
		public virtual DbSet<Product> Products { get; set; }
		public virtual DbSet<User> Users { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Category>()
				.HasMany(e => e.Products)
				.WithRequired(e => e.Category)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<GuestBook>()
				.Property(e => e.CellPhone)
				.IsUnicode(false);
		}
	}
}

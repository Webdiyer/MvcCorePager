using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Webdiyer.MvcCorePagerDemo.Models
{
    public partial class DemoDbContext : DbContext
    {
        public virtual DbSet<Order> Orders { get; set; }

        public DemoDbContext(DbContextOptions options) : base(options) { }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK__Orders__C3905BCF9CEFF74B");

                entity.Property(e => e.OrderId).ValueGeneratedNever();

                entity.Property(e => e.CompanyName).HasColumnType("varchar(50)");

                entity.Property(e => e.CustomerId).HasColumnType("varchar(50)");

                entity.Property(e => e.EmployeeName).HasColumnType("varchar(50)");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");
            });
        }
    }
}
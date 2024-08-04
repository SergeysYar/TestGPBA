using System.Collections.Generic;
using System.Reflection.Emit;
using System;

using TestGPBA.Model;
using Microsoft.EntityFrameworkCore;

namespace TestGPBA
{
    public class AppDbContext : DbContext
    {
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Supplier>()
                .HasMany(s => s.Offers)
                .WithOne(o => o.Supplier)
                .HasForeignKey(o => o.SupplierId);

            base.OnModelCreating(modelBuilder);
        }
    }

}

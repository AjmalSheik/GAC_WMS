using GAC_WMS_RestApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace GAC_WMS_RestApi.DatabaseConfig
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<PurchaseOrderHeader> PurchaseOrderHeaders { get; set; }
        public DbSet<PurchaseOrderLine> PurchaseOrderLines { get; set; }
        public DbSet<SalesOrderHeader> SalesOrderHeaders { get; set; }
        public DbSet<SalesOrderLine> SalesOrderLines { get; set; }

        public DbSet<ShipmentAddress> ShipmentAddress { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //modelBuilder.Entity<PurchaseOrderHeader>().

            //.HasOne(po => po.Customer);
            ////.WithMany(c => c.PurchaseOrders) 
            ////.HasForeignKey(po => po.CustomerId);


            //modelBuilder.Entity<SalesOrderHeader>()
            //    .HasOne(so => so.Customer);
            ////.WithMany(c => c.SalesOrders) 
            ////.HasForeignKey(so => so.CustomerId); 


            //modelBuilder.Entity<PurchaseOrderLine>()
            //    .HasOne(pol => pol.Product);
            ////.WithMany(p => p.PurchaseOrderLines) 
            ////.HasForeignKey(pol => pol.ProductId);


            //modelBuilder.Entity<SalesOrderLine>()
              
            //    .HasOne(sol => sol.Product);
            ////.WithMany(p => p.SalesOrderLines) 
            ////.HasForeignKey(sol => sol.ProductId);

            modelBuilder.Entity<Product>()
              .HasIndex(u => u.SKU)
              .IsUnique();

            modelBuilder.Entity<SalesOrderHeader>()
            .HasOne(s => s.ShipmentAddress)
            .WithMany(sa => sa.SalesOrders)
            .HasForeignKey(s => s.ShipmentAddressId)
            .OnDelete(DeleteBehavior.Restrict);

        }
    }
}

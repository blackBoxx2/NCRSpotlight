using EntitiesLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace Plugins.DataStore.SQLite
{
    public class NCRContext:DbContext
    {
        public NCRContext(DbContextOptions<NCRContext> options)
            :base(options)
        {
            
        }


        #region Create Tables
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Representative> Representatives { get; set; }
        public DbSet<RoleRep>RoleReps { get; set; }
        public DbSet<QualityPortion> QualityPortions { get; set; }
        public DbSet<NCRLog> NCRLog { get; set; }
        public DbSet<NCRLogHistory> NCRLogHistory { get; set; }
        #endregion

        #region Create Relationships
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Suppliers
            modelBuilder.Entity<Supplier>()
                .HasMany<Product>(s => s.Products)
                .WithOne(p => p.Supplier)
                .HasForeignKey(p => p.SupplierID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Supplier>()
                .HasIndex(s => s.SupplierName)
                .IsUnique();

            //Products
            modelBuilder.Entity<Product>()
                .HasMany<QualityPortion>(p => p.QualityPortions)
                .WithOne(qp => qp.Product)
                .HasForeignKey(qp => qp.ProductID);

            modelBuilder.Entity<Product>()
                .HasIndex(p => new { p.SupplierID, p.Description })
                .IsUnique();

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.ProductNumber)
                .IsUnique();


            //Role
            modelBuilder.Entity<Role>()
                .HasMany<RoleRep>(r => r.RoleReps)
                .WithOne(rp => rp.Role)
                .HasForeignKey(rp => rp.RoleID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Role>()
                .HasIndex(r => r.RoleName)
                .IsUnique();
            
            //Representative Should be aanble to delete a rep without an issue no? Also if a rep is deleted, we wanna set the id to null
            modelBuilder.Entity<Representative>()
                .HasMany<RoleRep>(rep => rep.RoleReps)
                .WithOne(rp => rp.Representative)
                .HasForeignKey(rp => rp.RepresentativeID)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Representative>()
                .HasIndex(rep => new {rep.FirstName, rep.MiddleInitial, rep.LastName})
                .IsUnique();

            //Rolerep
            //modelBuilder.Entity<RoleRep>()
            //    .HasKey(rp => new { rp.RoleID, rp.RepresentativeID });


            //NCRLog likely sdhould be able to delete an NCR as an admin with no issues, this should also delete the history
            modelBuilder.Entity<NCRLog>()
                .HasMany<NCRLogHistory>(n => n.History)
                .WithOne(nh => nh.NCRLog)
                .HasForeignKey(nh => nh.NCRLogID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<QualityPortion>()
                .HasOne<RoleRep>(n => n.RoleRep)
                .WithMany(r => r.QualityPortions)
                .HasForeignKey(n => n.RoleRepID);
        }
        #endregion
    }
}

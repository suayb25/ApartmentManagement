using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ApartmentManagement.Models
{
    public class ApartmentManagementEntities:DbContext
    {
        public ApartmentManagementEntities() : base("ApartmentManagementEntities") {
            Database.SetInitializer(new SampleData());
            if (!Database.Exists())
            {
                Database.Initialize(true);
            }
        }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Renter> Renters { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<Bills> Bills { get; set; }
        public DbSet<Complaint> Complaints { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Renter>().HasIndex(l => l.email).IsUnique();
            modelBuilder.Entity<Owner>().HasIndex(l => l.email).IsUnique();
            modelBuilder.Entity<Building>().HasIndex(l => l.name).IsUnique();
  
            //base.OnModelCreating(modelBuilder);
        }
    }
}
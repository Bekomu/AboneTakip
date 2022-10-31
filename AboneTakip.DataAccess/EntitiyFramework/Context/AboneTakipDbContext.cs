using AboneTakip.Core.Entities.Abstract;
using AboneTakip.Entity.Concrete;
using AboneTakip.Mapping.EnitityMapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboneTakip.DataAccess.EntitiyFramework.Context
{

    public class AboneTakipDbContext : DbContext
    {
        public AboneTakipDbContext(DbContextOptions<AboneTakipDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Reading> Readings { get; set; }
        public DbSet<Volumetric> Volumetrics { get; set; }
        public DbSet<Invoice> Invoices { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IMapping).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            SetBaseProperties();
            return base.SaveChanges();

        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetBaseProperties();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void SetBaseProperties()
        {
            var entries = ChangeTracker.Entries<BaseEntity>();
            foreach (var entry in entries)
            {
                SetIfAdded(entry);
                SetIfModify(entry);
                SetIfDeleted(entry);
            }
        }

        private void SetIfModify(EntityEntry<BaseEntity> entityEntry)
        {
            if (entityEntry.State == EntityState.Modified)
            {
                entityEntry.Entity.Status = Core.Enums.Status.Modified;
            }

            entityEntry.Entity.ModifiedBy = " ";
            entityEntry.Entity.ModifiedDate = System.DateTime.Now;
        }

        private void SetIfAdded(EntityEntry<BaseEntity> entityEntry)
        {
            if (entityEntry.State == EntityState.Added)
            {
                entityEntry.Entity.Status = Core.Enums.Status.Added;
                entityEntry.Entity.CreatedBy = " ";
                entityEntry.Entity.CreatedDate = System.DateTime.Now;
            }
        }

        private void SetIfDeleted(EntityEntry<BaseEntity> entityEntry)
        {
            if (entityEntry.State == EntityState.Deleted)
            {
                entityEntry.State = EntityState.Modified;
                entityEntry.Entity.Status = Core.Enums.Status.Deleted;
                entityEntry.Entity.DeletedBy = " ";
                entityEntry.Entity.DeletedDate = System.DateTime.Now;
            }
        }
    }


}

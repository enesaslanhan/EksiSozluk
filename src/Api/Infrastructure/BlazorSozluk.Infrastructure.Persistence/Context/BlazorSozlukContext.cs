using BlazorSozluk.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Infrastructure.Persistence.Context
{
    public class BlazorSozlukContext:DbContext
    {
        public const string DEFAULT_SCHEMA = "dbo";
        public BlazorSozlukContext()
        {
            
        }
        public BlazorSozlukContext(DbContextOptions options):base(options)
        {
        }
        public DbSet<Entry> Entries { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<EntryVote> EnrtyVotes { get; set; }
        public DbSet<EntryFavorite> EnrtyFavorites { get; set; }
        public DbSet<EntryComment> EnrtyComments { get; set; }
        public DbSet<EntryCommentVote> EnrtyCommentVotes { get; set; }
        public DbSet<EntryCommentFavorite> EnrtyCommentFavorites { get; set; }
        public DbSet<EmailComfirmation> EmailComfirmations { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connStr = "Server=.;Database=blazorsozluk;Trusted_Connection=True;TrustServerCertificate=true;";
                optionsBuilder.UseSqlServer(connStr, opt =>
                {
                    opt.EnableRetryOnFailure();
                });
            }
        }
         
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override int SaveChanges()
        {
            OnBeforeSave();
            return base.SaveChanges();
        }
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSave();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            OnBeforeSave();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            OnBeforeSave();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void OnBeforeSave()
        {
            var addedEntities = ChangeTracker.Entries()
                                            .Where(i => i.State == EntityState.Added)
                                            .Select(i => (BaseEntity)i.Entity);
            PrepareAddedEntities(addedEntities);
        }
        private void PrepareAddedEntities(IEnumerable<BaseEntity> entities)
        {
            foreach (var entity in entities)
            {
                if(entity.CreatedDate==DateTime.MinValue)
                    entity.CreatedDate = DateTime.Now;
            }
        }


    }
}

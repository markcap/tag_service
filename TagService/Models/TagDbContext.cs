using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TagService.Models
{
    public class TagDbContext : DbContext
    {
        public TagDbContext(DbContextOptions<TagDbContext> options) : base(options)
        {
        }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<Tagging> Taggings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SetUpEntity(modelBuilder.Entity<Tag>());
            SetUpEntity(modelBuilder.Entity<Tagging>());

            base.OnModelCreating(modelBuilder);
        }

        private void SetUpEntity(EntityTypeBuilder<Tag> entity)
        {
            entity.HasIndex(t => t.TagName)
                  .IsUnique();
            entity.Property(t => t.ActiveFlag).HasDefaultValue(true);
            entity.HasIndex(t => t.ActiveFlag);
            entity.HasQueryFilter(t => (bool)t.ActiveFlag);
        }

        private void SetUpEntity(EntityTypeBuilder<Tagging> entity)
        {
            entity.HasIndex(t => new { t.TaggableId, t.TaggableType, t.Context });
            entity.Property(t => t.ActiveFlag).HasDefaultValue(true);
            entity.HasIndex(t => t.ActiveFlag);
            entity.HasQueryFilter(t => (bool)t.ActiveFlag);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void OnBeforeSaving()
        {
            var entries = ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                if (entry.Entity is ITrackable trackable)
                {
                    var now = DateTime.UtcNow;
                    var user = GetCurrentUser();
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            trackable.ModifiedDate = now;
                            trackable.ModifiedBy = user;
                            break;

                        case EntityState.Added:
                            trackable.CreatedDate = now;
                            trackable.CreatedBy = user;
                            trackable.ModifiedDate = now;
                            trackable.ModifiedBy = user;
                            break;
                    }
                }
            }
        }

        private string GetCurrentUser()
        {
            return "SystemUser"; // TODO implement logic when admin users are added to new services
        }
    }
}

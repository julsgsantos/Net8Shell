using BIZBOX.PSA.DOMAIN.Entities;
using BIZBOX.PSA.PERSISTENCE.Security;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;

namespace BIZBOX.PSA.PERSISTENCE.Context
{
    public class BPSABaseDbContext : DbContext
    {
        protected BPSABaseDbContext(DbContextOptions<BPSADbContext> options)
                : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var newEntities = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added)
                .Select(x => x.Entity).ToList();

            var newEnumerator = newEntities.GetEnumerator();
            while (newEnumerator.MoveNext())
            {
                var ent = newEnumerator.Current;
                bool isBaseEntityInt = IsTypeof<BaseEntity<int>>(ent);

                if (isBaseEntityInt)
                {
                    var entity = ent as BaseEntity<int>;
                    if (entity != null)
                    {
                        entity.CreatedById = !string.IsNullOrEmpty(entity.CreatedById) ? entity.CreatedById : !string.IsNullOrEmpty(IdentityHelper.UserId) ? IdentityHelper.UserId : "n/a";
                        entity.CreatedBy = !string.IsNullOrEmpty(entity.CreatedBy) ? entity.CreatedBy : !string.IsNullOrEmpty(IdentityHelper.Email) ? IdentityHelper.Email : "n/a";
                    }
                }
                else
                {
                    var entity = ent as BaseEntity<long>;
                    if (entity != null)
                    {
                        entity.CreatedById = !string.IsNullOrEmpty(entity.CreatedById) ? entity.CreatedById : !string.IsNullOrEmpty(IdentityHelper.UserId) ? IdentityHelper.UserId : "n/a";
                        entity.CreatedBy = !string.IsNullOrEmpty(entity.CreatedBy) ? entity.CreatedBy : !string.IsNullOrEmpty(IdentityHelper.Email) ? IdentityHelper.Email : "n/a";
                    }
                }
            }

            var modifiedEntities = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Modified)
                .Select(x => x.Entity).ToList();

            var modifiedEnumerator = modifiedEntities.GetEnumerator();
            while (modifiedEnumerator.MoveNext())
            {
                var ent = modifiedEnumerator.Current;
                bool isBaseEntityInt = IsTypeof<BaseEntity<int>>(ent);

                if (isBaseEntityInt)
                {
                    var entity = ent as BaseEntity<int>;
                    if (entity != null)
                    {
                        entity.LastUpdatedAt = DateTime.UtcNow;
                        entity.LastUpdatedById = !string.IsNullOrEmpty(IdentityHelper.UserId) ? IdentityHelper.UserId : "n/a";
                        entity.LastUpdatedBy = !string.IsNullOrEmpty(IdentityHelper.Email) ? IdentityHelper.Email : "n/a";
                    }
                }
                else
                {
                    var entity = ent as BaseEntity<long>;
                    if (entity != null)
                    {
                        entity.LastUpdatedAt = DateTime.UtcNow;
                        entity.LastUpdatedById = !string.IsNullOrEmpty(IdentityHelper.UserId) ? IdentityHelper.UserId : "n/a";
                        entity.LastUpdatedBy = !string.IsNullOrEmpty(IdentityHelper.Email) ? IdentityHelper.Email : "n/a";
                    }
                }
            }

            return await base.SaveChangesAsync(true, cancellationToken);
        }

        bool IsTypeof<T>(object t)
        {
            return t is T;
        }
    }
}

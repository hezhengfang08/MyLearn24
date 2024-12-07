using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MySelf.Zhihu.Core.Common;
using MySelf.Zhihu.UseCases.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.Infrastructure.Data.Interceptors
{
    public class AuditEntityInterceptor(IUser currentUser):SaveChangesInterceptor
    {
        public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
        {
            UpdateEntities(eventData.Context);
            return base.SavedChanges(eventData, result);
        }
        public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            UpdateEntities(eventData.Context);
            return base.SavedChangesAsync(eventData, result, cancellationToken);
        }
        public void UpdateEntities(DbContext? context)
        {
          if(context == null) { return; }
          foreach( var entity in context.ChangeTracker.Entries<BaseAuditEntity>())
            {
                if (entity.State is not (EntityState.Added or EntityState.Modified)) continue;
                var utcNow = DateTimeOffset.UtcNow;
                if (entity.State == EntityState.Added)
                    entity.Entity.CreatedAt = utcNow;
                else
                    entity.Entity.LastModifiedAt = utcNow;
            }
            foreach (var entity in context.ChangeTracker.Entries<AuditWithUserEntity>())
            {
                if (entity.State is not (EntityState.Added or EntityState.Modified)) continue;

                if (currentUser.Id is null) continue;

                if (entity.State == EntityState.Added)
                    entity.Entity.CreatedBy = currentUser.Id;
                else
                    entity.Entity.LastModifiedBy = currentUser.Id;
            }
        }

    }
}

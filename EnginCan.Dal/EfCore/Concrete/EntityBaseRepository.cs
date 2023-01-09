using EnginCan.Dal.EfCore.Abstract;
using EnginCan.Entity.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace EnginCan.Dal.EfCore.Concrete
{
    public class EntityBaseRepository<T> : HttpContextAccessor, IEntityBaseRepository<T> where T : class, IEntity, new()
    {
        private readonly DbContext _context;

        public EntityBaseRepository(DbContext context)
        {
            _context = context;
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }

        public virtual IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsQueryable();
        }

        public virtual IQueryable<T> GetAll(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        public virtual int Count()
        {
            return _context.Set<T>().Count();
        }

        public virtual IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return query.AsQueryable();
        }

        public T GetSingle(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().FirstOrDefault(predicate);
        }

        public T GetSingleAsNoTracking(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().AsNoTracking().FirstOrDefault(predicate);
        }

        public T GetSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return query.Where(predicate).FirstOrDefault();
        }

        public virtual IQueryable<T> FindByAsNoTracking(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().AsNoTracking().Where(predicate);
        }

        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return query.Where(predicate);
        }

        public void DetachEntity(T entity)
        {
            _context.Entry(entity).State = EntityState.Detached;
        }

        public T GetOriginal(T entity)
        {
            return (T)_context.Entry(entity).OriginalValues.ToObject();
        }

        public bool IsHaveForeign(T entity)
        {
            using (var transaction = BeginTransaction())
            {
                try
                {
                    Delete(entity);
                    Commit();
                    transaction.Rollback();
                    return false;
                }
                catch { return true; }
            }
        }

        public virtual void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public virtual void AddRange(IEnumerable<T> entityList)
        {
            _context.Set<T>().AddRange(entityList);
        }

        public virtual Task AddRangeAsync(IEnumerable<T> entityList)
        {
            return _context.Set<T>().AddRangeAsync(entityList);
        }

        public virtual void Update(T entity)
        {
            _context.Update<T>(entity);
        }

        public virtual void Delete(T entity)
        {
            if (entity is BaseEntity baseLog)
            {
                int.TryParse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "userId")?.Value, out int uId);
                baseLog.LastUpdatedUserId = (uId == 0 ? (int?)null : uId);
                baseLog.DataStatus = DataStatus.Deleted;
                baseLog.LastUpdatedAt = DateTime.Now;

                _context.Update<T>(entity);
            }
            else
            {
                _context.Remove<T>(entity);
            }
        }

        public virtual void DeleteWhere(Expression<Func<T, bool>> predicate)
        {
            foreach (var entity in _context.Set<T>().Where(predicate))
            {
                if (entity is BaseEntity baseLog)
                {
                    int.TryParse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "userId")?.Value, out int id);
                    baseLog.LastUpdatedUserId = (id == 0 ? (int?)null : id);
                    baseLog.DataStatus = DataStatus.Deleted;
                    baseLog.LastUpdatedAt = DateTime.Now;

                    _context.Update<T>(entity);
                }
                else
                {
                    _context.Remove<T>(entity);
                }
            }
        }

        public virtual void DeleteWhereDeactivate(Expression<Func<T, bool>> predicate)
        {
            foreach (var entity in _context.Set<T>().Where(predicate))
            {
                if (entity is BaseEntity baseLog)
                {
                    int.TryParse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "userId")?.Value, out int id);
                    baseLog.LastUpdatedUserId = (id == 0 ? (int?)null : id);
                    baseLog.DataStatus = DataStatus.DeActivated;
                    baseLog.LastUpdatedAt = DateTime.Now;

                    _context.Update<T>(entity);
                }
                else
                {
                    _context.Remove<T>(entity);
                }
            }
        }

        public virtual void DeleteWhereAsNoTracking(Expression<Func<T, bool>> predicate)
        {
            foreach (var entity in _context.Set<T>().AsNoTracking().Where(predicate))
            {
                if (entity is BaseEntity baseLog)
                {
                    int.TryParse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "userId")?.Value, out int id);
                    baseLog.LastUpdatedUserId = (id == 0 ? (int?)null : id);
                    baseLog.DataStatus = DataStatus.Deleted;
                    baseLog.LastUpdatedAt = DateTime.Now;

                    _context.Update<T>(entity);
                }
                else
                {
                    _context.Remove<T>(entity);
                }
            }
        }

        public virtual void Commit()
        {
            OnBeforeSaving();
            _context.SaveChanges();
        }

        public virtual void CommitHard()
        {
            OnBeforeSavingHard();
            _context.SaveChanges();
        }

        public virtual Task<int> CommitAsync()
        {
            OnBeforeSaving();
            return _context.SaveChangesAsync();
        }

        private void OnBeforeSavingHard()
        {
            if (HttpContext != null)
            {
                DateTime now = DateTime.Now;
                int.TryParse(HttpContext?.User.Claims.FirstOrDefault(x => x.Type == "userId")?.Value, out int id);
                int? userId = (id == 0 ? (int?)null : id);
                foreach (var entry in _context.ChangeTracker.Entries().Where(a => a.Entity is BaseEntity))
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                        case EntityState.Detached:
                            ((BaseEntity)entry.Entity).CreatedAt = now;
                            ((BaseEntity)entry.Entity).CreatedUserId = userId;

                            if (((BaseEntity)entry.Entity).DataStatus == 0)
                                ((BaseEntity)entry.Entity).DataStatus = DataStatus.Activated;

                            // Ekleme yaparken string alanları otomatik trimliyoruz
                            foreach (var entity in entry.CurrentValues.Properties.Where(a => a.ClrType == typeof(string)))
                                entry.CurrentValues[entity.Name] = entry.CurrentValues[entity.Name]?.ToString()?.Trim();

                            break;

                        case EntityState.Modified:
                            ((BaseEntity)entry.Entity).LastUpdatedAt = now;
                            ((BaseEntity)entry.Entity).LastUpdatedUserId = userId;

                            // Güncelleme yaparken string alanları otomatik trimliyoruz
                            foreach (var entity in entry.CurrentValues.Properties.Where(a => a.ClrType == typeof(string)))
                                entry.CurrentValues[entity.Name] = entry.CurrentValues[entity.Name]?.ToString()?.Trim();
                            break;

                        case EntityState.Deleted:
                            ((BaseEntity)entry.Entity).LastUpdatedAt = now;
                            ((BaseEntity)entry.Entity).LastUpdatedUserId = userId;
                            break;
                    }
                }
            }
        }

        private void OnBeforeSaving()
        {
            if (HttpContext != null)
            {
                DateTime now = DateTime.Now;
                int.TryParse(HttpContext?.User.Claims.FirstOrDefault(x => x.Type == "userId")?.Value, out int id);
                int? userId = (id == 0 ? (int?)null : id);
                foreach (var entry in _context.ChangeTracker.Entries().Where(a => a.Entity is BaseEntity))
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            ((BaseEntity)entry.Entity).CreatedAt = now;
                            ((BaseEntity)entry.Entity).CreatedUserId = userId;
                            break;
                        case EntityState.Detached:
                            ((BaseEntity)entry.Entity).CreatedAt = now;
                            ((BaseEntity)entry.Entity).CreatedUserId = userId;

                            if (((BaseEntity)entry.Entity).DataStatus == 0)
                                ((BaseEntity)entry.Entity).DataStatus = DataStatus.Activated;

                            // Ekleme yaparken string alanları otomatik trimliyoruz
                            foreach (var entity in entry.CurrentValues.Properties.Where(a => a.ClrType == typeof(string)))
                                entry.CurrentValues[entity.Name] = entry.CurrentValues[entity.Name]?.ToString()?.Trim();

                            break;

                        case EntityState.Modified:
                            ((BaseEntity)entry.Entity).LastUpdatedAt = now;
                            ((BaseEntity)entry.Entity).LastUpdatedUserId = userId;

                            // Güncelleme yaparken string alanları otomatik trimliyoruz
                            foreach (var entity in entry.CurrentValues.Properties.Where(a => a.ClrType == typeof(string)))
                                entry.CurrentValues[entity.Name] = entry.CurrentValues[entity.Name]?.ToString()?.Trim();
                            break;

                        case EntityState.Deleted:
                            ((BaseEntity)entry.Entity).LastUpdatedAt = now;
                            ((BaseEntity)entry.Entity).LastUpdatedUserId = userId;
                            entry.State = EntityState.Modified;
                            break;
                    }
                }
            }
        }

        public void BulkInsert(IList<T> entityList)
        {

            if (entityList.Count > 0)
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _context.Set<T>().AddRange(entityList);
                        Commit();
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                    }
                }
        }

        public void BulkInsertWithNewScope(IList<T> entityList)
        {
            using (var transaction = new TransactionScope())
            {
                try
                {
                    _context.Set<T>().AddRange(entityList);
                    Commit();
                    transaction.Complete();
                }
                catch
                {
                    transaction.Dispose();
                }
            }
        }

        public async Task BulkInsertAsync(IList<T> entityList)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    await _context.Set<T>().AddRangeAsync(entityList);
                    await CommitAsync();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }
        }

        public void BulkUpdate(IList<T> entityList)
        {

            if (entityList.Count > 0)

                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _context.Set<T>().UpdateRange(entityList);
                        Commit();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                    }
                }
        }

        public async Task BulkUpdateAsync(IList<T> entityList)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Set<T>().UpdateRange(entityList);
                    await CommitAsync();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            }
        }

        public void BulkInsertOrUpdate(IList<T> entityList)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    foreach (var item in entityList)
                    {
                        var entry = _context.Entry(item);
                        switch (entry.State)
                        {
                            case EntityState.Detached:
                                if (entry.IsKeySet)
                                    _context.Update(item);
                                else
                                    _context.Add(item);
                                break;

                            case EntityState.Modified:
                                _context.Update(item);
                                break;

                            case EntityState.Added:
                                _context.Add(item);
                                break;

                            case EntityState.Unchanged:
                                //item already in db no need to do anything
                                break;

                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                    Commit();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            }
        }

        public async Task BulkInsertOrUpdateAsync(IList<T> entityList)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    foreach (var item in entityList)
                    {
                        var entry = _context.Entry(item);
                        switch (entry.State)
                        {
                            case EntityState.Detached:
                                if (entry.IsKeySet)
                                    _context.Update(item);
                                else
                                    _context.Add(item);
                                break;

                            case EntityState.Modified:
                                _context.Update(item);
                                break;

                            case EntityState.Added:
                                _context.Add(item);
                                break;

                            case EntityState.Unchanged:
                                //item already in db no need to do anything
                                break;

                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                    await CommitAsync();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            }
        }

        public void BulkSoftDelete(IList<T> entityList)
        {
            if (entityList.Count > 0)

                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        var willDeleted = new List<T>();

                        foreach (var entity in entityList)
                        {
                            if (entity is BaseEntity baseLog)
                            {
                                int.TryParse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "userId")?.Value, out int uId);
                                baseLog.LastUpdatedUserId = (uId == 0 ? (int?)null : uId);
                                baseLog.DataStatus = DataStatus.Deleted;
                                baseLog.LastUpdatedAt = DateTime.Now;
                            }

                            willDeleted.Add(entity);
                        }

                        _context.Set<T>().UpdateRange(willDeleted);
                        Commit();
                        transaction.Commit();

                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                    }
                }
        }

        public async Task BulkSoftDeleteAsync(IList<T> entityList)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var willDeleted = new List<T>();

                    foreach (var entity in entityList)
                    {
                        if (entity is BaseEntity baseLog)
                        {
                            int.TryParse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "userId")?.Value, out int uId);
                            baseLog.LastUpdatedUserId = (uId == 0 ? (int?)null : uId);
                            baseLog.DataStatus = DataStatus.Deleted;
                            baseLog.LastUpdatedAt = DateTime.Now;
                        }

                        willDeleted.Add(entity);
                    }

                    _context.Set<T>().UpdateRange(willDeleted);
                    await CommitAsync();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }
        }

        public void BulkDelete(IList<T> entityList)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    foreach (var entity in entityList)
                    {
                        if (entity is BaseEntity baseLog)
                        {
                            int.TryParse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "userId")?.Value, out int uId);
                            baseLog.LastUpdatedUserId = (uId == 0 ? (int?)null : uId);
                            baseLog.DataStatus = DataStatus.Deleted;
                            baseLog.LastUpdatedAt = DateTime.Now;
                            _context.Update<T>(entity);
                        }
                        else
                        {
                            _context.Remove<T>(entity);
                        }
                    }

                    Commit();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            }
        }

        public void BulkHardDelete(IList<T> entityList)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    foreach (var entity in entityList)
                    {
                        _context.Remove<T>(entity);
                    }

                    CommitHard();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            }
        }

        public async Task BulkDeleteAsync(IList<T> entityList)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    foreach (var entity in entityList)
                    {
                        if (entity is BaseEntity baseLog)
                        {
                            int.TryParse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "userId")?.Value, out int uId);
                            baseLog.LastUpdatedUserId = (uId == 0 ? (int?)null : uId);
                            baseLog.DataStatus = DataStatus.Deleted;
                            baseLog.LastUpdatedAt = DateTime.Now;
                            _context.Update<T>(entity);
                        }
                        else
                        {
                            _context.Remove<T>(entity);
                        }
                    }

                    await CommitAsync();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            }
        }
    }
}

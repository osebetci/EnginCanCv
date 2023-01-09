using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EnginCan.Dal.EfCore.Abstract
{
    public interface IEntityBaseRepository<T> where T : class, new()
    {

        IQueryable<T> GetAll();

        IQueryable<T> GetAll(params Expression<Func<T, object>>[] includeProperties);

        int Count();

        T GetSingleAsNoTracking(Expression<Func<T, bool>> predicate);

        T GetSingle(Expression<Func<T, bool>> predicate);

        T GetSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);

        IQueryable<T> FindByAsNoTracking(Expression<Func<T, bool>> predicate);

        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);

        IDbContextTransaction BeginTransaction();

        bool IsHaveForeign(T entity);

        void Add(T entity);

        void AddRange(IEnumerable<T> entityList);

        Task AddRangeAsync(IEnumerable<T> entityList);

        void Update(T entity);

        void Delete(T entity);

        void DeleteWhere(Expression<Func<T, bool>> predicate);

        void DeleteWhereAsNoTracking(Expression<Func<T, bool>> predicate);

        void DeleteWhereDeactivate(Expression<Func<T, bool>> predicate);

        void Commit();

        void CommitHard();

        void DetachEntity(T entity);

        T GetOriginal(T entity);

        Task<int> CommitAsync();

        void BulkInsert(IList<T> entityList);

        void BulkInsertWithNewScope(IList<T> entityList);

        Task BulkInsertAsync(IList<T> entityList);

        void BulkUpdate(IList<T> entityList);

        Task BulkUpdateAsync(IList<T> entityList);

        void BulkInsertOrUpdate(IList<T> entityList);

        Task BulkInsertOrUpdateAsync(IList<T> entityList);

        void BulkHardDelete(IList<T> entityList);

        void BulkDelete(IList<T> entityList);

        void BulkSoftDelete(IList<T> entityList);

        Task BulkSoftDeleteAsync(IList<T> entityList);

        Task BulkDeleteAsync(IList<T> entityList);
    }
}

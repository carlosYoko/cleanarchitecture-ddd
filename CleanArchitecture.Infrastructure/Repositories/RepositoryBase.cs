using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public class RepositoryBase<T> : IAsyncRepository<T> where T : BaseDomainModel
    {
        protected readonly StreamerDbContext? _context;

        public RepositoryBase(StreamerDbContext? context)
        {
            this._context = context;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()

        {
            return await this._context!.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await this._context!.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate = null,
                                                                Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                                                                string? includeString = null, bool disableTracking = true)
        {
            IQueryable<T> query = this._context!.Set<T>();
            if (disableTracking) query = query.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(includeString)) query = query.Include(includeString);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null) return await orderBy(query).ToListAsync();

            return await query.ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate = null,
                                                     Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                                                     string? includeString = null,
                                                     List<Expression<Func<T, object>>>? includes = null,
                                                     bool disableTracking = true)
        {
            IQueryable<T> query = this._context!.Set<T>();
            if (disableTracking) query = query.AsNoTracking();

            if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null) return await orderBy(query).ToListAsync();

            return await query.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await this._context!.Set<T>().FindAsync(id);
        }

        public async Task<T> AddAsync(T entity)
        {
            this._context!.Set<T>().Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            this._context!.Entry(entity).State = EntityState.Modified;
            await this._context.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            this._context!.Set<T>().Remove(entity);
            await this._context.SaveChangesAsync();
        }

        public void AddEntity(T entity)
        {
            this._context!.Set<T>().Add(entity);
        }

        public void UpdateEntity(T entity)
        {
            _context!.Set<T>().Attach(entity);
            this._context!.Entry(entity).State = EntityState.Modified;
        }

        public void DeleteEntity(T entity)
        {
            this._context!.Set<T>().Remove(entity);
        }
    }
}

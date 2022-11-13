using Microsoft.EntityFrameworkCore;
using Product.Data.Abstruct;
using Product.Data.Contexts;
using Product.Entities.Abstruct;
using System.Linq.Expressions;

namespace Product.Data.Concreate
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, IEntity, new()
    {
        public ProductContext context;

        public BaseRepository(ProductContext context)
        {
            this.context = context;
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            entity.ActiveFlag = true;
            entity.CreatedAt = DateTime.Now;

            var result = context.Entry(entity);
            result.State = EntityState.Added;
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public virtual async Task<List<T>?> AddRangeAsync(List<T> entities)
        {
            var now = DateTime.UtcNow;
            foreach (var entity in entities)
            {
                entity.ActiveFlag = true;
                entity.CreatedAt = now;
            }
            context.Set<T>().AddRange(entities);

            await context.SaveChangesAsync();
            return entities.ToList();
        }

        public virtual async Task<bool> DeleteAsync(T entity)
        {
            entity.ActiveFlag = false;

            var result = context.Entry(entity);
            result.State = EntityState.Modified;
            return await context.SaveChangesAsync() > 0;
        }

        public virtual async Task<bool> DeleteRangeAsync(List<T> entities)
        {
            foreach (T entity in entities)
                entity.ActiveFlag = false;

            context.Set<T>().UpdateRange(entities);
            return await context.SaveChangesAsync() > 0;
        }

        public virtual async Task<bool> ForceDeleteAsync(T entity)
        {
            context.Set<T>().Remove(entity);
            return await context.SaveChangesAsync() > 0;
        }

        public virtual async Task<bool> ForceDeleteRangeAsync(List<T> entities)
        {
            context.Set<T>().RemoveRange(entities);
            return await context.SaveChangesAsync() > 0;
        }

        public virtual async Task<List<T>?> GetAllAsync(Expression<Func<T, bool>>? filter = null)
        {
            var query = context.Set<T>().Where(x => x.ActiveFlag == true);
            if (filter == null)
                return await query.AsNoTracking().ToListAsync();
            else
                query = query.Where(filter);

            return await query.AsNoTracking().ToListAsync();
        }

        public virtual async Task<T?> GetAsync(Expression<Func<T, bool>> filter)
        {
            return await context.Set<T>()
                .Where(x => x.ActiveFlag == true)
                .Where(filter)
                .FirstOrDefaultAsync();
        }

        public virtual async Task<T?> GetByIdAsync(long id)
        {
            return await context.Set<T>().Where(x => x.Id == id && x.ActiveFlag == true).FirstOrDefaultAsync();
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            var result = context.Entry(entity);
            result.State = EntityState.Modified;
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> filter)
        {
            return await context.Set<T>()
                .Where(x => x.ActiveFlag == true)
                .AsNoTracking()
                .AnyAsync(filter);
        }
    }
}

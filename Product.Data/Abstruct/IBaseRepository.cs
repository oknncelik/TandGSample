using Product.Entities.Abstruct;
using System.Linq.Expressions;

namespace Product.Data.Abstruct
{
    public interface IBaseRepository<T> where T : class, IEntity, new()
    {
        Task<List<T>?> GetAllAsync(Expression<Func<T, bool>>? filter = null);
        Task<T?> GetByIdAsync(long id);
        Task<T> AddAsync(T value);
        Task<T> UpdateAsync(T value);
        Task<bool> DeleteAsync(T value);
        Task<List<T>?> AddRangeAsync(List<T> values);
        Task<bool> ForceDeleteAsync(T value);
        Task<bool> ForceDeleteRangeAsync(List<T> value);
        Task<bool> DeleteRangeAsync(List<T> values);
        Task<T?> GetAsync(Expression<Func<T, bool>> filter);
        Task<bool> AnyAsync(Expression<Func<T, bool>> filter);
    }
}

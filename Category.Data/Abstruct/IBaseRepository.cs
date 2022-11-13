using Category.Entities.Abstruct;
using System.Linq.Expressions;

namespace Category.Data.Abstruct
{
    public interface IBaseRepository<T> where T : class, IEntity, new()
    {
        /// <summary>
        /// Get All Entity Records
        /// Default OrderBy CreatedAt ASC
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderDir"></param>
        /// <param name="orderBy"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        Task<IList<T>?> GetAllAsync(Expression<Func<T, bool>>? filter = null, OrderBy orderDir = OrderBy.ASC, Expression<Func<T, object>>? orderBy = null, int? take = null);

        /// <summary>
        /// Get Entity by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T?> GetByIdAsync(string id);

        /// <summary>
        /// Add Entity Database
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<T?> AddAsync(T value);

        /// <summary>
        /// Add Entities Database
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        Task<IList<T>?> AddRangeAsync(IList<T> values);

        /// <summary>
        /// Update Entity
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<T?> UpdateAsync(T value);

        /// <summary>
        /// Soft Delete Entity. Not delete database. Change Active Flag
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(T value);

        /// <summary>
        /// Soft Delete Entities. Not delete database. Change Active Flag
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        Task<bool> DeleteRangeAsync(IList<T> values);

        /// <summary>
        /// Delete Entity Database.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<bool> ForceDeleteAsync(T value);

        /// <summary>
        /// Delete Entities Database.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<bool> ForceDeleteRangeAsync(IList<T> value);
        Task<T?> GetByFilterAsync(Expression<Func<T, bool>>? filter);
        Task<bool> AnyAsync(Expression<Func<T, bool>>? filter);
    }
}
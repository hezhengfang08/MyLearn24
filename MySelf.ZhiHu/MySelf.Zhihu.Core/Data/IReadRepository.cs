using MySelf.Zhihu.SharedKernel.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.Core.Data
{
    /// <summary>
    ///   <see cref="IRepository{T}" /> 用于查询 <typeparamref name="T" />.
    /// </summary>
    /// <typeparam name="T">该仓储操作的实体类型</typeparam>
    public interface IReadRepository<T>   where T : class,IAggregateRoot
    {
        /// <summary>
        ///    获取 Queryable 查询表达式
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetQuaryable();

        /// <summary>
        ///  查询具有指定主键的实体
        /// </summary>
        /// <typeparam name="TKey">主键的类型</typeparam>
        /// <param name="id">要查找的实体的主键值</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<T?> GetByIdAsync<TKey>(TKey id, CancellationToken cancellationToken = default) where TKey : notnull;

        /// <summary>
        /// 查询实体集合
        /// </summary>
        /// <param name="express"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<T>> GetListAsync(Expression<Func<T, bool>> express,CancellationToken cancellationToken = default);

        /// <summary>
        ///  统计符合条件的记录总数
        /// </summary>
        /// <param name="express"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> GetCountAsync(Expression<Func<T,bool>> express,CancellationToken cancellationToken = default);
    }
}

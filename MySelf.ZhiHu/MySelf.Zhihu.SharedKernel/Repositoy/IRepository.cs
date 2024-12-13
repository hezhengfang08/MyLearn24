using MySelf.Zhihu.SharedKernel.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.SharedKernel.Repositoy
{
    public interface IRepository<T> : IReadRepository<T> where T : class, IEntity, IAggregateRoot
    {
        /// <summary>
        ///     在数据库中添加实体
        /// </summary>
        /// <param name="entity">要添加的实体</param>
        /// <returns></returns>
        T Add(T entity);

        /// <summary>
        ///     在数据库中更新实体
        /// </summary>
        /// <param name="entity">要更新的实体</param>
        /// <returns></returns>
        void Update(T entity);

        /// <summary>
        ///     在数据库中删除实体
        /// </summary>
        /// <param name="entity">要删除的实体</param>
        /// <returns></returns>
        void Delete(T entity);

        /// <summary>
        ///     持久化实体到数据库
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

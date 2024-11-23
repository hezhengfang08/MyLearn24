using MySelf.Zero.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace MySelf.Zero.Domain.Repositories
{
    public interface ICategoryRepositroy : IRepository<CategoryEntity, long>
    {
        Task<List<CategoryEntity>> GetListOfIdArrayAsync(long[] userIds);
    }
}

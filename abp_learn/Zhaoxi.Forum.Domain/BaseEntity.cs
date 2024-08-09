
using Volo.Abp.Domain.Entities;

namespace Zhaoxi.Forum.Domain
{
    public class BaseEntity : Entity<long>
    {
        public DateTime CreationTime { get; set; } = DateTime.Now;
    }
}

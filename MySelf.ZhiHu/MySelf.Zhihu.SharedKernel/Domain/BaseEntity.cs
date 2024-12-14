using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.SharedKernel.Domain
{
    public abstract class BaseEntity<Tid>:IEntity<Tid>
    {
        private readonly List<BaseEvent> domainEvents = [];
        [NotMapped]
        public IReadOnlyCollection<BaseEvent> DomainEvents  => domainEvents.AsReadOnly();
        public Tid Id { get; set; } = default!;

        public void AddDomainEvent(BaseEvent domainEvent)
        {
            domainEvents.Add(domainEvent);
        }
        public void RemoveDomainEvent(BaseEvent domainEvent)
        {
            domainEvents.Remove(domainEvent);
        }
        public void ClearDomainEvents()
        {
            domainEvents.Clear();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.SharedKernel.Domain
{
    public interface IEntity;

    public interface IEntity<Tid> : IEntity
    {
        Tid Id { get; set; }
    }
}

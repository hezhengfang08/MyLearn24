using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.SharedKernel.Domain
{
    public class BaseEntity<Tid>:IEntity<Tid>
    {
        public Tid Id { get; set; } = default!;
    }
}

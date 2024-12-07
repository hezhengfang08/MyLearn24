using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.Core.Common
{
    public abstract class AuditWithUserEntity
    {
        public int? CreatedBy { get; set; }
        public int? LastModifiedBy { get; set; }
    }
}

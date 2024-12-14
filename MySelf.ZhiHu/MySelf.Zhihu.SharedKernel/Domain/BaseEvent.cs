using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.SharedKernel.Domain
{
    public class BaseEvent: INotification
    {
        public DateTimeOffset DateOccurred { get; protected set; } = DateTimeOffset.Now;
    }
}

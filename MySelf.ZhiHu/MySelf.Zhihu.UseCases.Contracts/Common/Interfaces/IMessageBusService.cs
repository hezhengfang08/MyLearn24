using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.UseCases.Contracts.Common.Interfaces
{
    public interface IMessageBusService
    {
        Task PushishAsync<TMessege>(TMessege message) where TMessege : class;
    }
}

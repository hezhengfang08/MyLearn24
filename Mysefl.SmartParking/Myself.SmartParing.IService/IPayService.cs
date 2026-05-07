using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.SmartParing.IService
{
    public interface IPayService
    {
        string Pay(string orderId, long amount);
        string CheckState(string orderId);
    }
}

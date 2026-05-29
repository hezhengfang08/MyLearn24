using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Client.IDAL
{
    public interface IContractAccess
    {
        string GetDatas(string key, string start, string end, int index, int size);

        string UpdateInfo(string ceJson);
        string DeleteInfo(int id);
        string ChangeState(int id, int state);

        string Execute(string ceJson);
        string Archived(string ceJson);
    }
}

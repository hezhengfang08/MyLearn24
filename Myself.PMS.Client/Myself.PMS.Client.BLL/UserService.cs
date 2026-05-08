using Myself.PMS.Client.Entities;
using Myself.PMS.Client.IBLL;
using Myself.PMS.Client.IDAL;
using Myself.PMS.Client.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Client.BLL
{
    public class UserService : IUserService
    {
        IUserAccess _userAccess;
        public UserService(IUserAccess userAccess)
        {
            _userAccess = userAccess;
        }

        public EmployeeEntity Login(string username, string password)
        {
            string json = _userAccess.Login(username, password);
            Result<EmployeeEntity> result = json.Deserialize<Result<EmployeeEntity>>();
            if (result.State != 200)
                throw new Exception(result.ExceptionMessage);


            //将Entity -》  Model     如果这样处理的话，需要将所有Model独立到一个程序集中
            return result.Data;
        }
        public EmployeeEntity Login(string username, string password, int test)
        {
            return null;
        }
    }
}

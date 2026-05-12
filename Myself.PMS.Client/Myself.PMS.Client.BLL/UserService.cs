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
    public class UserService : BaseService, IUserService
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

        public bool UpdatePassword(int id, string opd, string npd)
        {
            string json = _userAccess.UpdatePassword(id, opd, npd);
            Result<bool> result = json.Deserialize<Result<bool>>(   );
            if (result.State != 200)
                throw new Exception(result.ExceptionMessage);


            return result.Data;
        }



        public EmployeeEntity[] GetUsers(string key)
        {
            string json = _userAccess.GetUsers(key);
            Result<EmployeeEntity[]> result = json.Deserialize<Result<EmployeeEntity[]>>();
            if (result.State != 200)
                throw new Exception(result.ExceptionMessage);

            return result.Data;
        }

        public int DeleteUser(int id)
        {
            string result = _userAccess.DeleteUser(id);
            return this.GetResult<int>(result);
        }

        public bool LockUser(int id, int status)
        {
            string json = _userAccess.LockUser(id, status);
            return this.GetResult<bool>(json);
        }
        public int UpdateUser(EmployeeEntity entity)
        {
            string json = entity.Serialize();
            json = _userAccess.UpdateUser(json);

            return this.GetResult<int>(json);
        }
    }
}

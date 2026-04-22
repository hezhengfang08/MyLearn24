using Microsoft.EntityFrameworkCore;
using Myself.SmartParing.IService;
using Myself.SmartParking.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.SmartParking.Service
{
    public class UserService : BaseService, IUserService
    {
        public UserService(DbContext context) : base(context)
        {
        }

        public bool CheckUserName(string userName)
        {
            return this.Query<SysUser>(u => u.UserName == userName).Count() > 0;
        }

        //根据搜索关键词进行数据检索
        public IEnumerable<SysUser> GetUsers(string key)
        {
            // 需要将搜索关键词加入判断。。。。。。
            return this.Query<SysUser>(m =>

                string.IsNullOrEmpty(key) ||

                m.UserName.Contains(key) ||

                m.RealName.Contains(key) ||

                m.Address.Contains(key)
            );
        }

        public SysUser Login(string username, string password)
        {
            var users =
             this.Query<SysUser>(u => u.UserName == username && u.Password == password && u.Status == 1)
             .ToList();
            return users.FirstOrDefault();
        }
    }
}


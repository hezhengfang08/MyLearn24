using Microsoft.IdentityModel.Tokens;
using Myself.PMS.Server.Entities;
using Myself.PMS.Server.IService;
using SqlSugar;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Myself.PMS.Server.Service
{
    public class UserSerivce : IUserService
    {
        ISqlSugarClient _client;
        public UserSerivce(ISqlSugarClient client)
        {
            _client = client;
        }

        public bool CheckUserName(string username, int id)
        {
            // 新建的时候    id肯定为空  == 0
            // 编辑
            return _client.Queryable<SysEmployee>()
                .Any(u => u.UserName == username &&
                        u.EId != id);
        }

        public int ResetPassword(int id)
        {
            return _client.Updateable<SysEmployee>()
                 .SetColumns(e => new SysEmployee { Password = "123456" })
                 .Where(e => e.EId == id)
                 .ExecuteCommand();
        }
        public int SaveUserRoles(RoleUser[] roleUser)
        {
            _client.Deleteable<RoleUser>()
                .Where(ru => ru.UserId == roleUser[0].UserId)
                .ExecuteCommand();

            return _client.Insertable(roleUser).ExecuteCommand();
        }
        public int Delete(int id)
        {
            return _client.Deleteable<SysEmployee>()
                .In(id).ExecuteCommand();
        }
        public bool LockUser(int id, int status)
        {
            return _client.Updateable<SysEmployee>()
                 .SetColumns(e => new SysEmployee { Status = status })
                 .Where(e => e.EId == id)
                 .ExecuteCommand() > 0;


            // 第二种指定字段更新的方式:   两种方式  选一种处理
            // new { Status = status, EId = id }
            // SQL: update SysEmployee set Status=status wher eid=id;
            //
            //return _client.Updateable<SysEmployee>(new { Status = status, EId = id })
            //     .ExecuteCommand() > 0;
        }
        public SysEmployee? CheckLogin(string username, string password)
        {
            var es = _client.Queryable<SysEmployee>()
                .Where(e => e.UserName == username && e.Password == password)
                .ToList();

            var employee = es.FirstOrDefault();
            if (employee != null && this.AuthentationToken(username + password, out string token))
            {
                employee.Token = token;
            }
            return employee;
        }
        public int Update(SysEmployee employee)
        {
            int count = 0;
            if (employee.EId == 0)
            {
                // 插入
                // 用户ID生成一下
                int max = _client.Queryable<SysEmployee>()
                    .Where(e => e.EId.ToString().StartsWith(DateTime.Now.Year.ToString()))
                    .Max(e => e.EId);
                int new_id = 0;
                if (max == 0)
                {
                    new_id = DateTime.Now.Year * 1000 + 1;
                }
                {
                    max %= 1000;
                    new_id = DateTime.Now.Year * 1000 + max + 1;
                }

                employee.EId = new_id;
                count = _client.Insertable(employee).ExecuteCommand();
            }
            else
            {
                // 更新
                count = _client.Updateable(employee).ExecuteCommand();
            }
            return count;
        }
        public SysEmployee[] GetUsers(string key)
        {
            return _client.Queryable<SysEmployee>()
                .Where(e =>
                        string.IsNullOrEmpty(key) ||
                        e.UserName.Contains(key) ||
                        e.RealName.Contains(key) ||
                        e.Address.Contains(key)
                        )
                .Select(e => new SysEmployee()
                {
                    Roles = SqlFunc.Subqueryable<RoleUser>()
                    .Where(ru => ru.UserId == e.EId).ToList()
                })
                .ToArray();
        }
        public SysEmployee[] GetUsersByIds(int[] ids)
        {
            return _client.Queryable<SysEmployee>()
                .Where(e => ids.Contains(e.EId))
                .ToArray();
        }
        private bool AuthentationToken(string username, out string token)
        {
            token = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(username))
                    return false;

                var claims = new[]
                {
                    new Claim(ClaimTypes.Name,username)
                };

                // 密码   16位
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("123456123456123456中华人民共和国"));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var jwtToken = new JwtSecurityToken(
                    "webapi.cn",
                    "WebApi",
                    claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: credentials);

                token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdatePassword(int id, string old_password, string new_password)
        {
            var employee = _client.Queryable<SysEmployee>()
                 .Where(e => e.EId == id && e.Password == old_password)
                 .ToList().FirstOrDefault();
            if (employee == null)
                throw new Exception("没有匹配的用户的信息");

            employee.Password = new_password;

            // 更新的时候需要确认实体主键
            _client.Updateable(employee).ExecuteCommand();

            return true;
        }
    }
}

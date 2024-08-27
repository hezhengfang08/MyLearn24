using Microsoft.IdentityModel.Tokens;
using MySelf.PMS.Server.Entities;
using MySelf.PMS.Server.IService;
using SqlSugar;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MySelf.PMS.Server.Service
{
    public class UserService : IUserService
    {
        ISqlSugarClient _client;
        public UserService(ISqlSugarClient client) {
            this._client = client;
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
                }).ToArray();
        }
        private bool AuthentationToken(string username, out string token) { 
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
            catch (Exception ex) {
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

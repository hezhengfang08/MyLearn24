using MySelf.PMS.Server.Entities;
using MySelf.PMS.Server.IService;
using SqlSugar;

namespace MySelf.PMS.Server.Service
{
    public class UserService : IUserService
    {
        ISqlSugarClient _client;
        public UserService(ISqlSugarClient client) {
            this._client = client;
        }
        
        public bool CheckLogin(string username, string password)
        {
            var es = _client.Queryable<SysEmployee>()
               .Where(e => e.UserName == username && e.Password == password)
               .ToList();
            return es.Count() > 0;
        }
    }
}

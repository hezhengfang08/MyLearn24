using Myself.PMS.Server.Entities;
using Myself.PMS.Server.IService;
using SqlSugar;

namespace Myself.PMS.Server.Service
{
    public class UserSerivce : IUserService
    {
        ISqlSugarClient _client;
        public UserSerivce(ISqlSugarClient client)
        {
            _client = client;
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

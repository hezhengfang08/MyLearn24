using MySelf.QOSM.Models.Entities;
using MySelf.QOSM.Models.UIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.QOSM.IServices
{
    public interface IUserService
    {
        //管理员登录方法
        UserInfo AdminLogin(string username, string password);
        /// <summary>
        /// 查询用户分页
        /// </summary>
        /// <param name="keywords"></param>
        /// <param name="showDel"></param>
        /// <param name="startIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        PageModel<ViewUserInfo> GetUserList(string keywords,bool showDel, int startIndex, int pageSize);
        bool DeleteUser(int userId);
        bool RecoverUser(int userId);
        bool SaveUser(UserInfo userInfo);
        bool RemoveUser(int userId);    
        bool ExistUser(string userName);
        /// <summary>
        /// 启停
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        bool SetUserState(int userId, bool state);

    }
}

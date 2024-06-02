using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySelf.QOSM.IServices;
using MySelf.QOSM.Models.Entities;
using MySelf.QOSM.Models.UIModels;

namespace MySelf.QOSM.Services
{
    public class UserService :BaseService, IUserService
    {
        public UserInfo AdminLogin(string userName, string password)
        {
            string enPwd = GetMD5Str(password);

            var users = Query<UserInfo>(u => u.UserName == userName && u.UserPwd == enPwd && u.IsDeleted == false).Include(u => u.Role).ToList();
            if (users.Any())
            {
                UserInfo userInfo = users[0];
                return userInfo;
            }
            return null;
        }

        public bool DeleteUser(int userId)
        {
            return UpdateUserDelState(userId, 0, true);
        }
        private bool UpdateUserDelState(int userId, int delType, bool isDeleted) {
            var user = Find<UserInfo>(userId);
            bool resDel = false;
            if (delType == 0)
            {
                user.IsDeleted = isDeleted;
                dbContext.Entry(user).State = EntityState.Modified;
                dbContext.SaveChanges();
                resDel = true;
                Detach(user);
            }
            else
            {
                Delete(user);
                resDel = true;
            }
           return resDel;
        }

        public bool ExistUser(string userName)
        {
            return Query<UserInfo>(u=>u.UserName.Equals(userName) && u.IsDeleted == false).Any();    
        }

        public PageModel<ViewUserInfo> GetUserList(string keywords, bool showDel, int startIndex, int pageSize)
        {
            PageModel<ViewUserInfo> res = new PageModel<ViewUserInfo>();
            var userList = Query<ViewUserInfo>(u => u.IsDeleted == showDel);
            if(userList.Any()) { 
                if(!string.IsNullOrEmpty(keywords))
                {
                    userList = userList.Where(c=>c.UserName.Contains(keywords) || c.Phone.Contains(keywords));
                }
                if(userList.Any())
                {
                    res.DataList = userList.Skip(startIndex - 1).Take(pageSize).ToList(); 
                    res.TotalCount  = userList.Count();
                }
            }
            return res;
        }

        public bool RecoverUser(int userId)
        {
            return UpdateUserDelState(userId, 0, false);
        }

        public bool RemoveUser(int userId)
        {
            return UpdateUserDelState(userId, 1, true);
        }

        public bool SaveUser(UserInfo userInfo)
        {
            bool res = false;   
            if (userInfo.UserId == 0) {
                userInfo.UserPwd = GetMD5Str(userInfo.UserPwd);
                Insert(userInfo);
                if(userInfo.UserId >0)
                {
                    res = true;
                }
            }
            else
            {
                dbContext.Entry(userInfo).State = EntityState.Modified;
                if(!string.IsNullOrEmpty(userInfo.UserPwd))
                {
                    userInfo.UserPwd = GetMD5Str(userInfo.UserPwd);
                }
                else
                {
                    dbContext.Entry(userInfo).Property(u => u.UserPwd).IsModified = false;
                }
                Commit(); 
                if (dbContext.Entry(userInfo).State == EntityState.Unchanged)
                {
                    res = true;
                }
            }
            Detach(userInfo);
            return res;
        }

        public bool SetUserState(int userId, bool state)
        {
            bool blStat = false;
            var user = Find<UserInfo>(userId);
            if (user != null)
            {
                user.UserState = state;
                Update(user);   
                if(dbContext.Entry(user).State == EntityState.Unchanged)
                {
                    blStat = true;
                }
                Detach(user);
                
            }
            return blStat;
        }
    }
}



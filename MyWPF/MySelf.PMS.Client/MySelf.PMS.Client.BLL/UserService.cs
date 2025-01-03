﻿using MySelf.PMS.Client.Entities;
using MySelf.PMS.Client.IBLL;
using MySelf.PMS.Client.IDAL;
using MySelf.PMS.Client.Utils;
namespace MySelf.PMS.Client.BLL
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
            ResultEntiy<EmployeeEntity> result = JsonUtil.Deserializer<ResultEntiy<EmployeeEntity>>(json);
            if (result.State != ResultCode.Success)
                throw new Exception(result.Message);
            //将Entity -》  Model     如果这样处理的话，需要将所有Model独立到一个程序集中
            return result.Data;
        }
        public bool UpdatePassword(int id, string opd, string npd)
        {
            string json = _userAccess.UpdatePassword(id, opd, npd);
            ResultEntiy<bool> result = JsonUtil.Deserializer<ResultEntiy<bool>>(json);
            if (result.State != ResultCode.Success)
                throw new Exception(result.Message);
            return result.Data;
        }


        public EmployeeEntity[] GetUsers(string key)
        {
            string json = _userAccess.GetUsers(key);
            ResultEntiy<EmployeeEntity[]> result = JsonUtil.Deserializer<ResultEntiy<EmployeeEntity[]>>(json);
            if (result.State != ResultCode.Success)
                throw new Exception(result.Message);

            return result.Data;
        }
    }
}

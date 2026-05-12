using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Myself.PMS.Server.Entities;
using Myself.PMS.Server.IService;
using Myself.PMS.Server.Models;
using System.Net.Http.Headers;

namespace Myself.PMS.Server.Start.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("login")]
        public ActionResult Login([FromForm] string un, [FromForm] string pw)
        {
            // 结果封装
            // {
            //    "state":0
            //    "message":"异常消息"
            //    "data":{
            //        "EId":345345,
            //        "Name":"sefsegseg"
            //    }
            // }
            Result<SysEmployee> result = new Result<SysEmployee>();
            try
            {
                //throw new NotImplementedException();
                var data = _userService.CheckLogin(un, pw); 
                if (data == null)
                {
                    result.State = 404;
                    result.ExceptionMessage = "用户名或密码错误";
                }
                else
                    result.Data = data;
            }
            catch (Exception ex)
            {
                result.State = 500;
                result.ExceptionMessage = ex.Message;
            }
            return Ok(result);
        }
        [HttpGet("test")]
        [Authorize]
        public ActionResult Test()
        {
            return Ok("Zhaoxi");
        }
        [HttpPost("update_pwd")]
        [Authorize]
        public ActionResult UpdatePassword([FromForm] int id, [FromForm] string opd, [FromForm] string npd)
        {
            Result<bool> result = new Result<bool>();
            try
            {
                _userService.UpdatePassword(id, opd, npd);
                result.Data = true;
            }
            catch (Exception ex)
            {
                result.State = 500;
                result.ExceptionMessage = ex.Message;
            }

            return Ok(result);
        }
        [HttpGet("list/{key}")]
        [Authorize]
        public ActionResult GetUsers([FromRoute] string key)
        {
            Result<SysEmployee[]> result = new Result<SysEmployee[]>();
            try
            {
                key = key == "none" ? "" : key;
                result.Data = _userService.GetUsers(key);
            }
            catch (Exception ex)
            {
                result.State = 500;
                result.ExceptionMessage = ex.Message;
            }

            return Ok(result);
        }
        [HttpPost("update")]
        [Authorize]
        public ActionResult UpdateUser(SysEmployee entity)
        {
            Result<int> result = new Result<int>();
            try
            {
                var count = _userService.Update(entity);
                result.Data = count;
            }
            catch (Exception ex)
            {
                result.State = 500;
                result.ExceptionMessage = ex.Message;
            }
            return Ok(result);
        }

        [HttpGet("delete/{id}")]
        [Authorize]
        public ActionResult DeleteUser([FromRoute] int id)
        {
            Result<int> result = new Result<int>();
            try
            {
                var count = _userService.Delete(id);
                result.Data = count;
            }
            catch (Exception ex)
            {
                result.State = 500;
                result.ExceptionMessage = ex.Message;
            }
            return Ok(result);
        }

        // api/user/lock/2024001/0
        [HttpGet("lock/{id}/{status}")]
        //[Authorize]
        public ActionResult LockUser(int id, int status)
        {
            Result<bool> result = new Result<bool>();
            try
            {
                var state = _userService.LockUser(id, status);
                result.Data = state;
            }
            catch (Exception ex)
            {
                result.State = 500;
                result.ExceptionMessage = ex.Message;
            }
            return Ok(result);
        }
        [HttpGet("check/{id}/{name}")]
        [Authorize]
        public ActionResult CheckUserName(string name, int id)
        {
            Result<bool> result = new Result<bool>();
            try
            {
                var state = _userService.CheckUserName(name, id);
                result.Data = state;
            }
            catch (Exception ex)
            {
                result.State = 500;
                result.ExceptionMessage = ex.Message;
            }
            return Ok(result);
        }

        [HttpPost("save_roles")]
        [Authorize]
        public ActionResult SaveUserRoles(RoleUser[] roleUsers)
        {
            Result<int> result = new Result<int>();
            try
            {
                var count = _userService.SaveUserRoles(roleUsers);
                result.Data = count;
            }
            catch (Exception ex)
            {
                result.State = 500;
                result.ExceptionMessage = ex.Message;
            }
            return Ok(result);
        }

        [HttpPost("reset_pwd")]
        [Authorize]
        public ActionResult ResetPassword(int id)
        {
            Result<int> result = new Result<int>();
            try
            {
                var count = _userService.ResetPassword(id);
                result.Data = count;
            }
            catch (Exception ex)
            {
                result.State = 500;
                result.ExceptionMessage = ex.Message;
            }
            return Ok(result);
        }
    }
}


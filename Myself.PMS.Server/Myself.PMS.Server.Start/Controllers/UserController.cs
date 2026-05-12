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
    }
}


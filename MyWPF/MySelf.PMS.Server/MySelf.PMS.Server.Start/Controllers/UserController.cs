using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySelf.PMS.Server.Entities;
using MySelf.PMS.Server.IService;
using MySelf.PMS.Server.Models;
using SqlSugar;

namespace MySelf.PMS.Server.Start.Controllers
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
                    result.State = StateEnum.Faile;
                    result.Message = "用户名或密码错误";
                }
                else
                    result.Data = data;
            }
            catch (Exception ex)
            {
                result.State = StateEnum.Error;
                result.Message = ex.Message;
            }
            return Ok(result);
        }
        [HttpGet]
        public IActionResult Get(string un, string pw) {
            Result<SysEmployee> result = new Result<SysEmployee>();
            try
            {
                //throw new NotImplementedException();
                var data = _userService.CheckLogin(un, pw);
                if (data == null)
                {
                    result.State = StateEnum.Faile;
                    result.Message = "用户名或密码错误";
                }
                else
                    result.Data = data;
            }
            catch (Exception ex)
            {
                result.State = StateEnum.Error;
                result.Message = ex.Message;
            }
            return Ok(result);
        }
    }
}

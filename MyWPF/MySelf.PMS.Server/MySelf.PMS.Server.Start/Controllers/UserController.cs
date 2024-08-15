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

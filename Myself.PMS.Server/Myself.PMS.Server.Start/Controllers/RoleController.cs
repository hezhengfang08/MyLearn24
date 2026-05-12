using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Myself.PMS.Server.Entities;
using Myself.PMS.Server.IService;
using Myself.PMS.Server.Models;

namespace Myself.PMS.Server.Start.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        [HttpPost("list")]
        [Authorize]
        public ActionResult GetRoleByIds(int[] ids)
        {
            Result<SysRole[]> result = new Result<SysRole[]>();
            try
            {
                result.Data = _roleService.GetRoleByIds(ids);
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

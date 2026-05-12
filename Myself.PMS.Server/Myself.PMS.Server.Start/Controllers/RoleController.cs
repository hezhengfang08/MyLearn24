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
        // api/role/all/none
        [HttpGet("all/{key}")]
        //[Authorize]
        public ActionResult GetAllRoles(string key)
        {
            Result<SysRole[]> result = new Result<SysRole[]>();
            try
            {
                key = key == "none" ? "" : key;
                result.Data = _roleService.GetAllRoles(key);
            }
            catch (Exception ex)
            {
                result.State = 500;
                result.ExceptionMessage = ex.Message;
            }

            return Ok(result);
        }

        [HttpGet("check/{id}/{roleName}")]
        public ActionResult CheckRoleName(string roleName, int id)
        {
            Result<bool> result = new Result<bool>();
            try
            {
                result.Data = _roleService.CheckRoleName(roleName, id);
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
        public ActionResult UpdateRole(SysRole entity)
        {
            Result<int> result = new Result<int>();
            try
            {
                var count = _roleService.Update(entity);
                result.Data = count;
            }
            catch (Exception ex)
            {
                result.State = 500;
                result.ExceptionMessage = ex.Message;
            }
            return Ok(result);
        }
        [HttpPost("delete/{id}")]
        [Authorize]
        public ActionResult DeleteRole([FromRoute] int id)
        {
            Result<int> result = new Result<int>();
            try
            {
                var count = _roleService.DeleteRole(id);
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

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
    public class MenuController : ControllerBase
    {
        IMenuService _menuService;
        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }
        [HttpGet("all")]
        [Authorize]
        public ActionResult GetAllMenus()
        {
            Result<MenuEntity[]> result = new Result<MenuEntity[]>();
            try
            {
                var ms = _menuService.GetAllMenus();
                result.Data = ms.ToArray();
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

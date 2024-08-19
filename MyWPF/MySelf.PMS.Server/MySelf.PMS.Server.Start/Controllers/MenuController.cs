using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySelf.PMS.Server.Entities;
using MySelf.PMS.Server.IService;
using MySelf.PMS.Server.Models;

namespace MySelf.PMS.Server.Start.Controllers
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
        public ActionResult GetAllMenus() {
            Result<MenuEntity[]> result = new Result<MenuEntity[]>();
            try
            {
                var ms = _menuService.GetAllMenus();
                result.Data = ms.ToArray();
            }
            catch (Exception ex) {
                result.State = StateEnum.Error;
                result.Message = ex.Message;
            }
            return Ok(result);
        }
    }
}

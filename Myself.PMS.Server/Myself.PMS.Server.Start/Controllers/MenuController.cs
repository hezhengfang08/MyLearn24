using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
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
        // http://localhost:5037/api/menu/all?key=ewfwe
        // http://localhost:5037/api/menu/all?key=
        // http://localhost:5037/api/menu/all/ewfwe
        // http://localhost:5037/api/menu/all/none
        // http://localhost:5037/api/menu/all     (通过POST传  Body  Form)
        [HttpGet("all/{key}")]
        [Authorize]
        public ActionResult GetAllMenus([FromRoute] string key)
        {
            Result<MenuEntity[]> result = new Result<MenuEntity[]>();
            try
            {
                key = key == "none" ? "" : key;
                var ms = _menuService.GetAllMenus(key);
                result.Data = ms.ToArray();
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
        public ActionResult Update(MenuEntity menu)
        {
            Result<int> result = new Result<int>();
            try
            {
                var count = _menuService.Update(menu);
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
        public ActionResult DeleteMenu([FromRoute] string id)
        {
            Result<int> result = new Result<int>();
            try
            {
                var count = _menuService.Delete(id);
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

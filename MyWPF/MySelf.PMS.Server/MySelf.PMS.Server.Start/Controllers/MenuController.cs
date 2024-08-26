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
        // http://localhost:5037/api/menu/all?key=ewfwe
        // http://localhost:5037/api/menu/all?key=
        // http://localhost:5037/api/menu/all/ewfwe
        // http://localhost:5037/api/menu/all/none
        // http://localhost:5037/api/menu/all     (通过POST传  Body  Form)
        [HttpGet("all/{key}")]
        [Authorize]
        public ActionResult GetAllMenus([FromRoute]string key) {
            Result<MenuEntity[]> result = new Result<MenuEntity[]>();
            try
            {
                key = key == "none" ? "" : key;
                var ms = _menuService.GetAllMenus(key);
                result.Data = ms.ToArray();
            }
            catch (Exception ex) {
                result.State = StateEnum.Error;
                result.Message = ex.Message;
            }
            return Ok(result);
        }
        [HttpPut("update")]
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
                result.State = StateEnum.Error;
                result.Message = ex.Message;
            }
            return Ok(result);
        }
        [HttpPut("delete/{id}")]
        [Authorize]
        public ActionResult Delete([FromRoute] string id)
        {
            Result<int> result = new Result<int>();
            try
            {
                var count = _menuService.Delete(id);
                result.Data = count;
            }
            catch (Exception ex) { 
              result.State = StateEnum.Error;
              result.Message = ex.Message;
            }
            return Ok(result);  
        }
    }
}

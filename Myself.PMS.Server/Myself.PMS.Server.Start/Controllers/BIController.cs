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
    public class BIController : ControllerBase
    {
        IBaseInfoService _baseInfoService;
        public BIController(IBaseInfoService baseInfoService)
        {
            _baseInfoService = baseInfoService;
        }


        [HttpGet("page/{key}/{index}/{size}")]
        [Authorize]
        public ActionResult GetAllBaseInfo(string key, int index, int size)
        {
            Result<Page<BaseInfo[]>> result = new Result<Page<BaseInfo[]>>();
            try
            {
                key = (key == "none" ? "" : key);
                int total = 0;
                var bis = _baseInfoService.GetBaseInfos(key, index, size, ref total);

                // 添加一层分页信息
                Page<BaseInfo[]> page = new Page<BaseInfo[]>();
                page.PageIndex = index;
                page.PageSize = size;
                page.TotalCount = total;
                page.Data = bis;

                result.Data = page;
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

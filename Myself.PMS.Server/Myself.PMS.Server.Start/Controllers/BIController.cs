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
        [HttpPost("update")]
        public ActionResult UpdateInfo(BaseInfo baseInfo)
        {
            Result<int> result = new Result<int>();
            try
            {
                result.Data = _baseInfoService.UpdateBaseInfo(baseInfo);
            }
            catch (Exception ex)
            {
                result.State = 500;
                result.ExceptionMessage = ex.Message;
            }
            return Ok(result);
        }

        [HttpPost("delete/{id}")]
        public ActionResult DeleteInfo([FromRoute] int id)
        {
            Result<int> result = new Result<int>();
            try
            {
                result.Data = _baseInfoService.DeleteBaseInfo(id);
            }
            catch (Exception ex)
            {
                result.State = 500;
                result.ExceptionMessage = ex.Message;
            }
            return Ok(result);
        }


        [HttpPost("cancel/{id}")]
        [Authorize]
        public ActionResult CancelState([FromRoute] int id)
        {
            Result<int> result = new Result<int>();
            try
            {
                var count = _baseInfoService.CancelState(id);
                result.Data = count;
            }
            catch (Exception ex)
            {
                result.State = 500;
                result.ExceptionMessage = ex.Message;
            }
            return Ok(result);
        }
        [HttpPost("publish/{id}")]
        [Authorize]
        public ActionResult PublishState([FromRoute]int id)
        {
            Result<int> result = new Result<int>();
            try
            {
                var count = _baseInfoService.PublishState(id);
                result.Data = count;
            }
            catch (Exception ex)
            {
                result.State = 500;
                result.ExceptionMessage = ex.Message;
            }
            return Ok(result);
        }
        [HttpPost("revoke/{id}")]
        [Authorize]
        public ActionResult RevokeState([FromRoute]int id)
        {
            Result<int> result = new Result<int>();
            try
            {
                var count = _baseInfoService.RevokeState(id);
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

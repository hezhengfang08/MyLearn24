using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Myself.PMS.Server.Entities;
using Myself.PMS.Server.IService;
using Myself.PMS.Server.Models;

namespace Myself.PMS.Server.Start.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinanceController : ControllerBase
    {
        IFinanceService _financeService;
        public FinanceController(IFinanceService financeService)
        {
            _financeService = financeService;
        }

        [HttpGet("page/{key}/{start}/{end}/{index}/{size}")]
        public IActionResult GetDatas(string key,
            string start, string end,
            int index, int size)
        {
            Result<Page<IEEntity[]>> result = new Result<Page<IEEntity[]>>();
            try
            {
                key = (key == "none" ? "" : key);
                int total = 0;
                var datas = _financeService.GetDatas(key,
                    DateTime.Parse(start), DateTime.Parse(end),
                    index, size, ref total);
                Page<IEEntity[]> page = new Page<IEEntity[]>();
                page.PageIndex = index;
                page.PageSize = size;
                page.TotalCount = total;
                page.Data = datas;
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
        public IActionResult UpdateInfo(IEEntity iEEntity)
        {
            Result<int> result = new Result<int>();
            try
            {
                result.Data = _financeService.UpdateInfo(iEEntity);
            }
            catch (Exception ex)
            {
                result.State = 500;
                result.ExceptionMessage = ex.Message;
            }
            return Ok(result);
        }

        [HttpGet("delete/{id}")]
        public IActionResult DeleteInfo(int id)
        {
            Result<int> result = new Result<int>();
            try
            {
                result.Data = _financeService.DeleteInfo(id);
            }
            catch (Exception ex)
            {
                result.State = 500;
                result.ExceptionMessage = ex.Message;
            }
            return Ok(result);
        }

        [HttpGet("state/{id}/{state}")]
        public IActionResult ChangeState(int id, int state)
        {
            Result<int> result = new Result<int>();
            try
            {
                result.Data = _financeService.ChangeState(id, state);
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

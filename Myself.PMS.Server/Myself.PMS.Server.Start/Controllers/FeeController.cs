using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Myself.PMS.Server.Entities;
using Myself.PMS.Server.IService;
using Myself.PMS.Server.Models;

namespace Myself.PMS.Server.Start.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeeController : ControllerBase
    {
        IFeeService _feeService;
        public FeeController(IFeeService feeService)
        {
            _feeService = feeService;
        }

        [HttpGet("page/{key}/{index}/{size}")]
        public IActionResult GetFees(string key, int index, int size)
        {
            Result<Page<FeeEntity[]>> result = new Result<Page<FeeEntity[]>>();
            try
            {
                key = (key == "none" ? "" : key);
                int total = 0;
                var bis = _feeService.GetFees(key, index, size, ref total);
                Page<FeeEntity[]> page = new Page<FeeEntity[]>();
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

        [HttpGet("feemode")]
        public IActionResult GetFeeModes()
        {
            Result<FeeModeEntity[]> result = new Result<FeeModeEntity[]>();
            try
            {
                result.Data = _feeService.GetFeeModes();
            }
            catch (Exception ex)
            {
                result.State = 500;
                result.ExceptionMessage = ex.Message;
            }
            return Ok(result);
        }

        [HttpPost("update")]
        public IActionResult UpdateFeeInfo(FeeEntity fee)
        {
            Result<int> result = new Result<int>();
            try
            {
                result.Data = _feeService.UpdateFee(fee);
            }
            catch (Exception ex)
            {
                result.State = 500;
                result.ExceptionMessage = ex.Message;
            }
            return Ok(result);
        }

        [HttpGet("delete/{id}")]
        public ActionResult Delete(int id)
        {
            Result<int> result = new Result<int>();
            try
            {
                var count = _feeService.DeleteFee(id);
                result.Data = count;
            }
            catch (Exception ex)
            {
                result.State = 500;
                result.ExceptionMessage = ex.Message;
            }
            return Ok(result);
        }

        [HttpGet("state/{id}/{state}")]
        public ActionResult ChangeState(int id, int state)
        {
            Result<int> result = new Result<int>();
            try
            {
                var count = _feeService.ChangeFeeState(id, state);
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

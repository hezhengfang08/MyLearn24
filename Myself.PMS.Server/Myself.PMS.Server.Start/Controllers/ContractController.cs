using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Myself.PMS.Server.Entities;
using Myself.PMS.Server.IService;
using Myself.PMS.Server.Models;

namespace Myself.PMS.Server.Start.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController : ControllerBase
    {
        IContractService _contract;
        public ContractController(IContractService contract)
        {
            _contract = contract;
        }

        [HttpGet("page/{key}/{start}/{end}/{index}/{size}")]
        public IActionResult GetInfo(string key, string start, string end, int index, int size)
        {
            Result<Page<ContractEntity[]>> result = new Result<Page<ContractEntity[]>>();
            try
            {
                key = (key == "none" ? "" : key);
                int total = 0;
                var datas = _contract.GetDatas(key,
                    DateTime.Parse(start), DateTime.Parse(end),
                    index, size, ref total);
                Page<ContractEntity[]> page = new Page<ContractEntity[]>();
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
        public IActionResult UpdateInfo(ContractEntity ce)
        {
            Result<int> result = new Result<int>();
            try
            {
                result.Data = _contract.UpdateInfo(ce);
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
                result.Data = _contract.DeleteInfo(id);
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
                result.Data = _contract.ChangeState(id, state);
            }
            catch (Exception ex)
            {
                result.State = 500;
                result.ExceptionMessage = ex.Message;
            }
            return Ok(result);
        }

        [HttpPost("execute")]
        public IActionResult Execute(ContractEntity ce)
        {
            Result<int> result = new Result<int>();
            try
            {
                result.Data = _contract.Execute(ce);
            }
            catch (Exception ex)
            {
                result.State = 500;
                result.ExceptionMessage = ex.Message;
            }
            return Ok(result);
        }

        [HttpPost("archived")]
        public IActionResult Archived(ContractEntity ce)
        {
            Result<int> result = new Result<int>();
            try
            {
                result.Data = _contract.Archived(ce);
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

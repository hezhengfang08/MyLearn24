using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Myself.PMS.Server.Entities;
using Myself.PMS.Server.IService;
using Myself.PMS.Server.Models;

namespace Myself.PMS.Server.Start.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        IOwnerService _ownerService;
        public OwnerController(IOwnerService ownerService)
        {
            _ownerService = ownerService;
        }

        [HttpPost("page/{index}/{size}")]
        public ActionResult GetAllOwners(
            [FromBody] ConditionEntity[] conditions,
            [FromRoute] int index,
            [FromRoute] int size)
        {
            Result<Page<OwnerEntity[]>> result = new Result<Page<OwnerEntity[]>>();
            try
            {
                int total = 0;
                var os = _ownerService.GetOwners(conditions, index, size, ref total);

                // 添加一层分页信息
                Page<OwnerEntity[]> page = new Page<OwnerEntity[]>();
                page.PageIndex = index;
                page.PageSize = size;
                page.TotalCount = total;
                page.Data = os;

                result.Data = page;
            }
            catch (Exception ex)
            {
                result.State = 500;
                result.ExceptionMessage = ex.Message;
            }
            return Ok(result);
        }
        [HttpGet("quarters")]
        public ActionResult GetQuarters()
        {
            Result<QuarterEntity[]> result = new Result<QuarterEntity[]>();
            try
            {
                var list = _ownerService.GetQuarters();
                result.Data = list;
            }
            catch (Exception ex)
            {
                result.State = 500;
                result.ExceptionMessage = ex.Message;
            }
            return Ok(result);
        }

        [HttpGet("buildings")]
        public ActionResult GetBuidings()
        {
            Result<BuildingEntity[]> result = new Result<BuildingEntity[]>();
            try
            {
                var list = _ownerService.GetBuildings();
                result.Data = list;
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

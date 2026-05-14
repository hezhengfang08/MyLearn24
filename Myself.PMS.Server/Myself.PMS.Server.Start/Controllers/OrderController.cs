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
    public class OrderController : ControllerBase
    {
        IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("page/{key}/{index}/{size}")]
        [Authorize]
        public IActionResult GetOrders(string key, int index, int size)
        {
            Result<Page<OrderEntity[]>> result = new Result<Page<OrderEntity[]>>();
            try
            {
                key = (key == "none" ? "" : key);
                int total = 0;
                var os = _orderService.GetOrders(key, index, size, ref total);

                // 添加一层分页信息
                Page<OrderEntity[]> page = new Page<OrderEntity[]>();
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
    }
}

using Microsoft.AspNetCore.Mvc;
using NetFlow.Application.Netsis.Orders;
using NetFlow.Application.Netsis.Products;
using NetFlow.Domain.Common.Pagination;
using NetFlow.Domain.Identity;

namespace NetFlow.Api.Controllers.Netsis
{
    [ApiController]
    [Route("api/netsis/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly OrderService _service;
        private readonly CurrentUser _currentUser;

        public OrdersController(OrderService service, CurrentUser currentUser)
        {
            _service = service;
            _currentUser = currentUser;
        }

        [HttpGet("list")]
        public async Task<IActionResult> List([FromQuery] short orderType, [FromQuery] PagedRequest request)
        {
            var data = await _service.GetOrders(orderType,request);
            return Ok(data);
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetFlow.Api.Dto;
using NetFlow.Application.Netsis.Shipments;
using NetFlow.Domain.Common.Pagination;
using NetFlow.Domain.Identity;
using NetFlow.Domain.Netsis.Shipments;

namespace NetFlow.Api.Controllers.Netsis
{
    [ApiController]
    [Route("api/netsis/shipments")]
    [Authorize]
    public class ShipmentsController : ControllerBase
    {
        private readonly ShipmentService _service;
        private readonly CurrentUser _currentUser;

        public ShipmentsController(ShipmentService service, CurrentUser currentUser)
        {
            _service = service;
            _currentUser = currentUser;
        }

        [HttpGet("shippable-orders")]
        public async Task<IActionResult> ShippableOrders([FromQuery] PagedRequest pagedRequest)
        {
            var filter = new ShipmentShippableOrderFilter(
                null,
                null,
                null,
                null,
                false
            );

            var data = await _service.GetShippableOrders(filter);
            return Ok(data);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using NetFlow.Api.Dto;
using NetFlow.Application.Shipping;
using NetFlow.Domain.Shipping;

namespace NetFlow.Api.Controllers
{
    [ApiController]
    [Route("api/shipments")]
    public class ShipmentsController : ControllerBase
    {
        private readonly ShipmentService _service;

        public ShipmentsController(ShipmentService service)
        {
            _service = service;
        }

        [HttpPost("shippable-orders")]
        public async Task<IActionResult> ShippableOrders([FromBody] ShipmentShippableOrderRequestDto dto)
        {
            var filter = new ShipmentShippableOrderFilter(
                dto.Cari,
                dto.StartDate,
                dto.EndDate,
                dto.Depo,
                dto.HasBalance
            );

            var data = await _service.GetShippableOrders(filter);
            return Ok(data);
        }
    }
}

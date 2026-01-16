using Microsoft.AspNetCore.Mvc;
using NetFlow.Api.Dto;
using NetFlow.Application.Common.Pagination;
using NetFlow.Application.Netsis.Shipments;
using NetFlow.Application.Netsis.Warehouses;
using NetFlow.Domain.Identity;
using NetFlow.Domain.Netsis.Shipments;

namespace NetFlow.Api.Controllers.Netsis
{
    [ApiController]
    [Route("api/netsis/warehouses")]
    public class WarehousesController : ControllerBase
    {
        private readonly WarehouseService _service;
        private readonly CurrentUser _currentUser;

        public WarehousesController(WarehouseService service, CurrentUser currentUser)
        {
            _service = service;
            _currentUser = currentUser;
        }

        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            var data = await _service.GetWarehouses();
            return Ok(data);
        }
    }
}

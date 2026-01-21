using Microsoft.AspNetCore.Mvc;
using NetFlow.Application.Netsis.Customers;
using NetFlow.Application.Netsis.Warehouses;
using NetFlow.Domain.Common.Pagination;
using NetFlow.Domain.Identity;

namespace NetFlow.Api.Controllers.Netsis
{
    [ApiController]
    [Route("api/netsis/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerService _service;
        private readonly CurrentUser _currentUser;

        public CustomersController(CustomerService service, CurrentUser currentUser)
        {
            _service = service;
            _currentUser = currentUser;
        }

        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            var data = await _service.GetCustomers();
            return Ok(data);
        }

        [HttpGet("pagedList")]
        public async Task<IActionResult> List([FromQuery] PagedRequest request)
        {
            var data = await _service.GetPagedAsync(request);
            return Ok(data);
        }
    }
}

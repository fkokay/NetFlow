using Microsoft.AspNetCore.Mvc;
using NetFlow.Application.Netsis.Products;
using NetFlow.Application.Netsis.Warehouses;
using NetFlow.Domain.Identity;

namespace NetFlow.Api.Controllers.Netsis
{
    [ApiController]
    [Route("api/netsis/products")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _service;
        private readonly CurrentUser _currentUser;

        public ProductsController(ProductService service, CurrentUser currentUser)
        {
            _service = service;
            _currentUser = currentUser;
        }

        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            var data = await _service.GetProducts();
            return Ok(data);
        }
    }
}

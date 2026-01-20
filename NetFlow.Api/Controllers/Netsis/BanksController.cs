using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetFlow.Application.Netsis.Banks;
using NetFlow.Application.Netsis.Products;
using NetFlow.Domain.Identity;

namespace NetFlow.Api.Controllers.Netsis
{
    [Route("api/netsis/banks")]
    [ApiController]
    public class BanksController : ControllerBase
    {
        private readonly BankService _service;
        private readonly CurrentUser _currentUser;

        public BanksController(BankService service, CurrentUser currentUser)
        {
            _service = service;
            _currentUser = currentUser;
        }

        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            var data = await _service.GetBanks();
            return Ok(data);
        }
    }
}

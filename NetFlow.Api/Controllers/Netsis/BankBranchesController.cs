using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetFlow.Application.Netsis.BankBranches;
using NetFlow.Application.Netsis.Banks;
using NetFlow.Domain.Identity;

namespace NetFlow.Api.Controllers.Netsis
{
    [Route("api/netsis/bankbranches")]
    [ApiController]
    public class BankBranchesController : ControllerBase
    {
        private readonly BankBranchService _service;
        private readonly CurrentUser _currentUser;

        public BankBranchesController(BankBranchService service, CurrentUser currentUser)
        {
            _service = service;
            _currentUser = currentUser;
        }
        [HttpGet("list")]
        public async Task<IActionResult> List([FromQuery] string bankCode)
        {
            var data = await _service.GetBankBranches(bankCode);
            return Ok(data);
        }

    }
}

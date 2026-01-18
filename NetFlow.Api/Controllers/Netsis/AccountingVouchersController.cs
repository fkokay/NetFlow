using Microsoft.AspNetCore.Mvc;
using NetFlow.Application.Netsis.AccountingVouchers;
using NetFlow.Application.Netsis.Orders;
using NetFlow.Domain.Common.Pagination;
using NetFlow.Domain.Identity;

namespace NetFlow.Api.Controllers.Netsis
{
    [ApiController]
    [Route("api/netsis/accounting-vouchers")]
    public class AccountingVouchersController : ControllerBase
    {
        private readonly AccountingVoucherService _service;
        private readonly CurrentUser _currentUser;

        public AccountingVouchersController(AccountingVoucherService service, CurrentUser currentUser)
        {
            _service = service;
            _currentUser = currentUser;
        }

        [HttpGet("list")]
        public async Task<IActionResult> List([FromQuery] string accountCode, [FromQuery] PagedRequest request)
        {
            var data = await _service.GetAccountingVouchers(accountCode, request);
            return Ok(data);
        }
    }
}

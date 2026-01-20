using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetFlow.Application.Netsis.ExpenseAccountCodes;
using NetFlow.Application.Netsis.Orders;
using NetFlow.Domain.Common.Pagination;
using NetFlow.Domain.Identity;

namespace NetFlow.Api.Controllers.Netsis
{
    [Route("api/netsis/expense-account-codes")]
    [ApiController]
    public class ExpenseAccountCodeController : ControllerBase
    {
        private readonly ExpenseAccountCodeService _service;
        private readonly CurrentUser _currentUser;

        public ExpenseAccountCodeController(ExpenseAccountCodeService service, CurrentUser currentUser)
        {
            _service = service;
            _currentUser = currentUser;
        }

        [HttpGet("list")]
        public async Task<IActionResult> List([FromQuery] PagedRequest request)
        {
            var data = await _service.GetExpenseAccountCodes(request);
            return Ok(data);
        }
    }
}

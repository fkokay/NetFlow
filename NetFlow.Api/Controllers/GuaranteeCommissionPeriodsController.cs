using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetFlow.Application.GuaranteeCommissions;
using NetFlow.Domain.Common.Pagination;
using NetFlow.ReadModel.GuaranteeCommissionPeriods;
using NetFlow.ReadModel.GuaranteeCommissions;

namespace NetFlow.Api.Controllers
{
    [Route("api/guarantee-commission-periods")]
    [ApiController]
    public class GuaranteeCommissionPeriodsController : ControllerBase
    {
        private readonly GuaranteeCommissionPeriodReadService _read;

        public GuaranteeCommissionPeriodsController(GuaranteeCommissionPeriodReadService read)
        {
            _read = read;
        }

        [HttpGet]
        public async Task<IActionResult> List() => Ok(await _read.ListAsync());
    }
}

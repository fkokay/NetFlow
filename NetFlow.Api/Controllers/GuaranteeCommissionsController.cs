using Microsoft.AspNetCore.Mvc;
using NetFlow.Domain.Common.Pagination;
using NetFlow.ReadModel.GuaranteeCommissions;

namespace NetFlow.Api.Controllers
{
    [ApiController]
    [Route("api/guarantee-commissions")]
    public class GuaranteeCommissionsController : ControllerBase
    {
        private readonly GuaranteeCommissionReadService _read;

        public GuaranteeCommissionsController(GuaranteeCommissionReadService read)
        {
            _read = read;
        }

        // GET api/guarantee-commissions?guaranteeId=5
        [HttpGet]
        public async Task<IActionResult> List([FromQuery] int guaranteeId, [FromQuery] PagedRequest pagedRequest) => Ok(await _read.ListAsync(guaranteeId, pagedRequest));

        // GET api/guarantee-commissions/12
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var row = await _read.GetAsync(id);
            return row is null ? NotFound() : Ok(row);
        }
    }
}


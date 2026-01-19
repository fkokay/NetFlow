using Microsoft.AspNetCore.Mvc;
using NetFlow.Application.GuaranteeCommissions;
using NetFlow.Domain.Common.Pagination;
using NetFlow.ReadModel.GuaranteeCommissions;

namespace NetFlow.Api.Controllers
{
    [ApiController]
    [Route("api/guarantee-commissions")]
    public class GuaranteeCommissionsController : ControllerBase
    {
        private readonly GuaranteeCommissionReadService _read;
        private readonly GuaranteeCommissionWriteService _write;

        public GuaranteeCommissionsController(GuaranteeCommissionReadService read, GuaranteeCommissionWriteService write)
        {
            _read = read;
            _write = write;
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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateGuaranteeCommissionRequest request)
        {
            var id = await _write.CreateAsync(request);

            return CreatedAtAction(
                nameof(Get),
                new { id },
                null);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] EditGuaranteeCommissionRequest request)
        {
            var id = await _write.EditAsync(request);
            return CreatedAtAction(
                nameof(Get),
                new { id },
                null);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _write.DeleteAsync(id);
            return Ok();
        }
    }
}


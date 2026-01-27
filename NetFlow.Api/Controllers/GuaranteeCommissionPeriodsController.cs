using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetFlow.Application.GuaranteeCommissionPeriods;
using NetFlow.Application.GuaranteeCommissions;
using NetFlow.Application.Guarantees;
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
        private readonly GuaranteeCommissionPeriodWriteService _write;

        public GuaranteeCommissionPeriodsController(GuaranteeCommissionPeriodReadService read, GuaranteeCommissionPeriodWriteService write)
        {
            _read = read;
            _write = write;
        }

        [HttpGet]
        public async Task<IActionResult> List() => Ok(await _read.ListAsync());

        [HttpGet("list")]
        public async Task<IActionResult> List([FromQuery] PagedRequest pagedRequest)
        {
            return Ok(await _read.ListAsync(pagedRequest));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var row = await _read.GetAsync(id);
            return row is null ? NotFound() : Ok(row);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateGuaranteeCommissionPeriodRequest request)
        {
            var id = await _write.CreateAsync(request);

            return CreatedAtAction(
                nameof(Get),
                new { id },
                null);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] EditGuaranteeCommissionPeriodRequest request)
        {
            var id = await _write.EditAsync(request);
            return CreatedAtAction(
                nameof(Get),
                new { id },
                null);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _write.DeleteAsync(id);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }
    }
}

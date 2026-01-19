using Microsoft.AspNetCore.Mvc;
using NetFlow.Application.Guarantees;
using NetFlow.Application.Roles;
using NetFlow.Domain.Common.Pagination;
using NetFlow.Domain.Identity;
using NetFlow.ReadModel.Guarantees;

namespace NetFlow.Api.Controllers
{
    [ApiController]
    [Route("api/guarantees")]
    public class GuaranteesController : ControllerBase
    {
        private readonly GuaranteeReadService _read;
        private readonly GuaranteeWriteService _write;
        protected readonly CurrentUser _current;

        public GuaranteesController(GuaranteeReadService read, CurrentUser current, GuaranteeWriteService write)
        {
            _read = read;
            _current = current;
            _write = write;
        }

        [HttpGet]
        public async Task<IActionResult> List([FromQuery] PagedRequest pagedRequest)
        {
            if (_current.User == null)
            {
                return NotFound();
            }

            return Ok(await _read.ListAsync(_current.User.Firm.Id, pagedRequest));
        }

        [HttpGet("expiring")]
        public async Task<IActionResult> Expiring([FromQuery] PagedRequest pagedRequest)
        {
            if (_current.User == null)
            {
                return NotFound();
            }

            return Ok(await _read.ListAsync(_current.User.Firm.Id, pagedRequest, expiring: true));
        }

        [HttpGet("active")]
        public async Task<IActionResult> Active([FromQuery] PagedRequest pagedRequest)
        {
            if (_current.User == null)
            {
                return NotFound();
            }

            return Ok(await _read.ListAsync(_current.User.Firm.Id, pagedRequest, isActive: true));
        }

        [HttpGet("refunded")]
        public async Task<IActionResult> Refunded([FromQuery] PagedRequest pagedRequest)
        {
            if (_current.User == null)
            {
                return NotFound();
            }

            return Ok(await _read.ListAsync(_current.User.Firm.Id, pagedRequest, isRefunded: true));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var row = await _read.GetAsync(id);
            return row is null ? NotFound() : Ok(row);
        }


        [HttpPut]
        public async Task<IActionResult> Update([FromBody] EditGuaranteeRequest request)
        {
            var id = await _write.EditAsync(request);
            return CreatedAtAction(
                nameof(Get),
                new { id },
                null);
        }
    }
}

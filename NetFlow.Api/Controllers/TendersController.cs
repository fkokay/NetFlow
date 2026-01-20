using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetFlow.Application.Roles;
using NetFlow.Application.Tenders;
using NetFlow.Domain.Common.Pagination;
using NetFlow.Domain.Identity;
using NetFlow.ReadModel.Tenders;

namespace NetFlow.Api.Controllers
{
    [ApiController]
    [Route("api/tenders")]
    public class TendersController : ControllerBase
    {
        protected readonly CurrentUser _current;
        private readonly TenderReadService _read;
        private readonly TenderWriteService _write;

        public TendersController(CurrentUser current, TenderReadService read, TenderWriteService write)
        {
            _current = current;
            _read = read;
            _write = write;
        }

        // GET api/tenders
        [HttpGet]
        public async Task<IActionResult> List([FromQuery] PagedRequest pagedRequest)
        {
            if (_current.User == null)
            {
                return NotFound();
            }

            return Ok(await _read.ListAsync(_current.User.Firm.Id,pagedRequest));
        }

        // GET api/tenders/10
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var row = await _read.GetAsync(id);
            return row is null ? NotFound() : Ok(row);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] EditTenderRequest request)
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

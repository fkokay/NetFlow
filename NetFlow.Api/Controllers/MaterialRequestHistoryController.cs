using Microsoft.AspNetCore.Mvc;
using NetFlow.Application.MaterialRequestHistories;
using NetFlow.Application.MaterialRequestItems;
using NetFlow.Domain.Common.Pagination;
using NetFlow.Domain.Identity;
using NetFlow.ReadModel.MaterialRequestHistories;
using NetFlow.ReadModel.MaterialRequestItems;

namespace NetFlow.Api.Controllers
{
    [ApiController]
    [Route("api/material-request-histories")]
    public class MaterialRequestHistoryController : ControllerBase
    {
        private readonly CurrentUser _current;
        private readonly MaterialRequestHistoryReadService _read;
        private readonly MaterialRequestHistoryWriteService _write;

        public MaterialRequestHistoryController(CurrentUser current, MaterialRequestHistoryReadService read, MaterialRequestHistoryWriteService write)
        {
            _current = current;
            _read = read;
            _write = write;
        }

        [HttpGet]
        public async Task<IActionResult> List([FromQuery] int materialRequestId, [FromQuery] PagedRequest pagedRequest) => Ok(await _read.ListAsync(materialRequestId, pagedRequest));

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var row = await _read.GetAsync(id);
            return row is null ? NotFound() : Ok(row);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMaterialRequestHistoryRequest request)
        {
            if (_current.User == null)
            {
                return NotFound();
            }

            request.ActionByUserId = _current.User.Id.Value;
            var id = await _write.CreateAsync(request);

            return CreatedAtAction(
                nameof(Get),
                new { id },
                null);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] EditMaterialRequestHistoryRequest request)
        {
            if (_current.User == null)
            {
                return NotFound();
            }
            request.ActionByUserId = _current.User.Id.Value;
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

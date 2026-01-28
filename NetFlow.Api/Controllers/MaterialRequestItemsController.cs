using Microsoft.AspNetCore.Mvc;
using NetFlow.Application.Guarantees;
using NetFlow.Application.MaterialRequestItems;
using NetFlow.Application.MaterialRequests;
using NetFlow.Application.Users;
using NetFlow.Domain.Common.Pagination;
using NetFlow.Domain.Identity;
using NetFlow.ReadModel.MaterialRequestItems;
using NetFlow.ReadModel.Requests;
using NetFlow.ReadModel.Users;

namespace NetFlow.Api.Controllers
{
    [ApiController]
    [Route("api/material-request-items")]
    public class MaterialRequestItemsController : ControllerBase
    {
        private readonly CurrentUser _current;
        private readonly MaterialRequestItemReadService _read;
        private readonly MaterialRequestItemWriteService _write;

        public MaterialRequestItemsController(CurrentUser current, MaterialRequestItemReadService read, MaterialRequestItemWriteService write)
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
        public async Task<IActionResult> Create([FromBody] CreateMaterialRequestItemRequest request)
        {
            var id = await _write.CreateAsync(request);

            return CreatedAtAction(
                nameof(Get),
                new { id },
                null);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] EditMaterialRequestItemRequest request)
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

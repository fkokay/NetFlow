using Microsoft.AspNetCore.Mvc;
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

        public MaterialRequestItemsController(CurrentUser current, MaterialRequestItemReadService read)
        {
            _current = current;
            _read = read;
        }

        [HttpGet]
        public async Task<IActionResult> List([FromQuery] int materialRequestId, [FromQuery] PagedRequest pagedRequest) => Ok(await _read.ListAsync(materialRequestId, pagedRequest));

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var row = await _read.GetAsync(id);
            return row is null ? NotFound() : Ok(row);
        }

    }
}

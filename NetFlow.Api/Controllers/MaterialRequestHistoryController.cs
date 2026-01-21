using Microsoft.AspNetCore.Mvc;
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

        public MaterialRequestHistoryController(CurrentUser current, MaterialRequestHistoryReadService read)
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

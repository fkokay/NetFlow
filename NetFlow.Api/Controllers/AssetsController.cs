using Microsoft.AspNetCore.Mvc;
using NetFlow.Application.Common.Pagination;
using NetFlow.Domain.Common;
using NetFlow.Domain.Identity;
using NetFlow.ReadModel.Assets;

namespace NetFlow.Api.Controllers
{
    [ApiController]
    [Route("api/assets")]
    public class AssetsController : ControllerBase
    {
        private readonly AssetReadService _read;
        protected readonly CurrentUser _current;

        public AssetsController(AssetReadService read, CurrentUser current)
        {
            _read = read;
            _current = current;
        }

        // GET api/assets?tenderId=5
        [HttpGet]
        public async Task<IActionResult> List([FromQuery] int? tenderId,[FromQuery] PagedRequest pagedRequest)
        {
            return Ok(await _read.ListAsync(tenderId, pagedRequest));
        }

        // GET api/assets/15
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var row = await _read.GetAsync(id);
            return row is null ? NotFound() : Ok(row);
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using NetFlow.Domain.Common;
using NetFlow.ReadModel.Assets;

namespace NetFlow.Api.Controllers
{
    [ApiController]
    [Route("api/assets")]
    public class AssetsController : ControllerBase
    {
        private readonly AssetReadService _read;

        public AssetsController(AssetReadService read)
        {
            _read = read;
        }

        // GET api/assets?tenderId=5
        [HttpGet]
        public async Task<IActionResult> List([FromQuery] int? tenderId)
            => Ok(await _read.ListAsync(tenderId));

        // GET api/assets/15
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var row = await _read.GetAsync(id);
            return row is null ? NotFound() : Ok(row);
        }

    }
}

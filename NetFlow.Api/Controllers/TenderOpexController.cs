using Microsoft.AspNetCore.Mvc;
using NetFlow.Domain.Common;
using NetFlow.Domain.Common.Pagination;
using NetFlow.ReadModel.TenderOpex;

namespace NetFlow.Api.Controllers
{
    [ApiController]
    [Route("api/tender-opex")]
    public class TenderOpexController : ControllerBase
    {
        private readonly TenderOpexReadService _read;

        public TenderOpexController(TenderOpexReadService read)
        {
            _read = read;
        }

        // GET api/tender-opex?tenderId=5
        [HttpGet]
        public async Task<IActionResult> List([FromQuery] int tenderId, [FromQuery] PagedRequest pagedRequest) => Ok(await _read.ListAsync(tenderId, pagedRequest));

        // GET api/tender-opex/12
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var row = await _read.GetAsync(id);
            return row is null ? NotFound() : Ok(row);
        }
    }
}

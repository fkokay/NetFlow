using Microsoft.AspNetCore.Mvc;
using NetFlow.Domain.Common;
using NetFlow.Domain.Common.Pagination;

namespace NetFlow.Api.Controllers
{
    [ApiController]
    [Route("api/tender-capex")]
    public class TenderCapexController : ControllerBase
    {
        private readonly TenderCapexReadService _read;

        public TenderCapexController(TenderCapexReadService read)
        {
            _read = read;
        }

        // GET api/tender-capex?tenderId=5
        [HttpGet]
        public async Task<IActionResult> List([FromQuery] int tenderId, [FromQuery] PagedRequest pagedRequest) => Ok(await _read.ListAsync(tenderId, pagedRequest));


        // GET api/tender-capex/12
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var row = await _read.GetAsync(id);
            return row is null ? NotFound() : Ok(row);
        }
    }
}

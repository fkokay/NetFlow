using Microsoft.AspNetCore.Mvc;
using NetFlow.Domain.Common;
using NetFlow.Domain.Common.Pagination;
using NetFlow.ReadModel.TenderReaktif;

namespace NetFlow.Api.Controllers
{
    [ApiController]
    [Route("api/tender-reaktif")]
    public class TenderReaktifController : ControllerBase
    {
        private readonly TenderReaktifReadService _read;

        public TenderReaktifController(TenderReaktifReadService read)
        {
            _read = read;
        }

        // GET api/tender-reaktif?tenderId=5
        [HttpGet]
        public async Task<IActionResult> List([FromQuery] int tenderId, [FromQuery] PagedRequest pagedRequest) => Ok(await _read.ListAsync(tenderId, pagedRequest));


        // GET api/tender-reaktif/12
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var row = await _read.GetAsync(id);
            return row is null ? NotFound() : Ok(row);
        }
    }
}

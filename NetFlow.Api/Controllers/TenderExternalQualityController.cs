using Microsoft.AspNetCore.Mvc;
using NetFlow.Application.Common.Pagination;
using NetFlow.Domain.Common;
using NetFlow.ReadModel.TenderExternalQuality;

namespace NetFlow.Api.Controllers
{
    [ApiController]
    [Route("api/tender-external-quality")]
    public class TenderExternalQualityController : ControllerBase
    {
        private readonly TenderExternalQualityReadService _read;

        public TenderExternalQualityController(TenderExternalQualityReadService read)
        {
            _read = read;
        }

        // GET api/tender-external-quality?tenderId=5
        [HttpGet]
        public async Task<IActionResult> List([FromQuery] int tenderId, [FromQuery] PagedRequest pagedRequest) => Ok(await _read.ListAsync(tenderId, pagedRequest));


        // GET api/tender-external-quality/12
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var row = await _read.GetAsync(id);
            return row is null ? NotFound() : Ok(row);
        }
    }
}

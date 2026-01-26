using Microsoft.AspNetCore.Mvc;
using NetFlow.Application.TenderPersonnels;
using NetFlow.Domain.Common.Pagination;
using NetFlow.ReadModel.TenderPersonnel;

namespace NetFlow.Api.Controllers
{
    [Route("api/tender-personnel")]
    [ApiController]
    public class TenderPersonnelController : ControllerBase
    {
        private readonly TenderPersonnelReadService _read;
        private readonly TenderPersonnelWriteService _write;
        public TenderPersonnelController(TenderPersonnelReadService read, TenderPersonnelWriteService write)
        {
            _read = read;
            _write = write;
        }

        // GET api/tender-personnel?tenderId=5
        [HttpGet]
        public async Task<IActionResult> List([FromQuery] int tenderId, [FromQuery] PagedRequest pagedRequest) => Ok(await _read.ListAsync(tenderId, pagedRequest));

        // GET api/tender-personnel/12
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var row = await _read.GetAsync(id);
            return row is null ? NotFound() : Ok(row);
        }

        [HttpPut("{tenderId}")]
        public async Task<IActionResult> UpdateTenderPersonnel(int tenderId, [FromBody] EditTenderPersonnelRequest request)
        {
            if (tenderId != request.TenderId)
                return BadRequest("TenderId uyuşmuyor");

            await _write.EditAsync(request);
            return NoContent();
        }
    }
}

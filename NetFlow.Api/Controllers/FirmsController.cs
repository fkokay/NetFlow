using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetFlow.Application.Firms;
using NetFlow.Domain.Common.Pagination;
using NetFlow.Domain.Identity;
using NetFlow.ReadModel.Assets;
using NetFlow.ReadModel.Firms;

namespace NetFlow.Api.Controllers
{
    [ApiController]
    [Route("api/firms")]
    public class FirmsController : Controller
    {
        private readonly FirmReadService _read;
        private readonly FirmWriteService _write;

        public FirmsController(FirmReadService read, FirmWriteService write)
        {
            _read = read;
            _write = write;
        }

        // GET api/firms
        [HttpGet]
        public async Task<IActionResult> PadegList([FromQuery] PagedRequest pagedRequest) => Ok(await _read.ListAsync(pagedRequest));


        [HttpGet("list")]
        [AllowAnonymous]
        public async Task<IActionResult> List() => Ok(await _read.GetFirmListAsync());

        // GET api/firms/15
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var row = await _read.GetAsync(id);
            return row is null ? NotFound() : Ok(row);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFirmRequest request)
        {
            var id = await _write.CreateAsync(request);

            return CreatedAtAction(
                nameof(Get),
                new { id },
                null);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] EditFirmRequest request)
        {
            var id = await _write.EditAsync(request);
            return CreatedAtAction(
                nameof(Get),
                new { id },
                null);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _write.DeleteAsync(id);
            return Ok();
        }
    }
}

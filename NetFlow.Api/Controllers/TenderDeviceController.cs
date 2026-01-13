using Microsoft.AspNetCore.Mvc;
using NetFlow.Domain.Common;
using NetFlow.ReadModel.TenderDevices;

namespace NetFlow.Api.Controllers
{
    [ApiController]
    [Route("api/tender-devices")]
    public class TenderDeviceController : ControllerBase
    {
        private readonly TenderDeviceReadService _read;

        public TenderDeviceController(TenderDeviceReadService read)
        {
            _read = read;
        }

        // GET api/tender-devices?tenderId=5
        [HttpGet]
        public async Task<IActionResult> List([FromQuery] int tenderId)
            => Ok(await _read.ListAsync(tenderId));

        // GET api/tender-devices/12
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var row = await _read.GetAsync(id);
            return row is null ? NotFound() : Ok(row);
        }
    }
}

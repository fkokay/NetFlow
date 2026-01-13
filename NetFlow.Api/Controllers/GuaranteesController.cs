using Microsoft.AspNetCore.Mvc;
using NetFlow.ReadModel.Guarantees;

namespace NetFlow.Api.Controllers
{
    [ApiController]
    [Route("api/guarantees")]
    public class GuaranteesController : ControllerBase
    {
        private readonly GuaranteeReadService _read;

        public GuaranteesController(GuaranteeReadService read)
        {
            _read = read;
        }

        [HttpGet]
        public async Task<IActionResult> List([FromQuery] int? firmId)
            => Ok(await _read.ListAsync(firmId));

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var row = await _read.GetAsync(id);
            return row is null ? NotFound() : Ok(row);
        }
    }
}

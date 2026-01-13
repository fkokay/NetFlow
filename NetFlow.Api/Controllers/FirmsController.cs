using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetFlow.ReadModel.Assets;
using NetFlow.ReadModel.Firms;

namespace NetFlow.Api.Controllers
{
    [ApiController]
    [Route("api/firms")]
    public class FirmsController : Controller
    {
        private readonly FirmReadService _read;

        public FirmsController(FirmReadService read)
        {
            _read = read;
        }

        // GET api/firms
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> List()
            => Ok(await _read.ListAsync());

        // GET api/firms/15
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var row = await _read.GetAsync(id);
            return row is null ? NotFound() : Ok(row);
        }
    }
}

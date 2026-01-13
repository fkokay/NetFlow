using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetFlow.Domain.Identity;
using NetFlow.ReadModel.Tenders;

namespace NetFlow.Api.Controllers
{
    [ApiController]
    [Route("api/tenders")]
    [Authorize]
    public class TendersController : ControllerBase
    {
        protected readonly CurrentUser _current;
        private readonly TenderReadService _read;

        public TendersController(CurrentUser current,TenderReadService read)
        {
            _current = current;
            _read = read;
        }

        // GET api/tenders?firmId=1
        [HttpGet]
        public async Task<IActionResult> List() => Ok(await _read.ListAsync(_current.User.Firm.Id));

        // GET api/tenders/10
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var row = await _read.GetAsync(id);
            return row is null ? NotFound() : Ok(row);
        }
    }
}

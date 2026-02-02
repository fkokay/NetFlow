using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetFlow.Application.Guarantees;
using NetFlow.Domain.Common.Pagination;
using NetFlow.Domain.Identity;
using NetFlow.ReadModel.Guarantees;
using NetFlow.ReadModel.ServiceForms;

namespace NetFlow.Api.Controllers
{
    [ApiController]
    [Route("api/service-forms")]
    public class ServiceFormController : ControllerBase
    {
        private readonly ServiceFormReadService _read;
        protected readonly CurrentUser _current;

        public ServiceFormController(ServiceFormReadService read, CurrentUser current)
        {
            _read = read;
            _current = current;
        }

        [HttpGet("list")]
        public async Task<IActionResult> List([FromQuery] PagedRequest pagedRequest)
        {
            if (_current.User == null)
            {
                return NotFound();
            }

            return Ok(await _read.ListAsync(_current.User.Id.Value,pagedRequest));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var row = await _read.GetAsync(id);
            return row is null ? NotFound() : Ok(row);
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetFlow.Domain.Common.Pagination;
using NetFlow.Domain.Identity;
using NetFlow.ReadModel.ServiceFormHistories;
using NetFlow.ReadModel.ServiceForms;

namespace NetFlow.Api.Controllers
{
    [ApiController]
    [Route("api/service-form-histories")]
    public class ServiceFormHistoryController : ControllerBase
    {
        private readonly ServiceFormHistoryReadService _read;
        protected readonly CurrentUser _current;

        public ServiceFormHistoryController(ServiceFormHistoryReadService read, CurrentUser current)
        {
            _read = read;
            _current = current;
        }

        [HttpGet]
        public async Task<IActionResult> List([FromQuery] int serviceFormId,[FromQuery] PagedRequest pagedRequest)
        {
            if (_current.User == null)
            {
                return NotFound();
            }

            return Ok(await _read.ListAsync(_current.User.Id.Value, serviceFormId, pagedRequest));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var row = await _read.GetAsync(id);
            return row is null ? NotFound() : Ok(row);
        }
    }
}

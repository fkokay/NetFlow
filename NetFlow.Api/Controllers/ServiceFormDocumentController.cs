using Microsoft.AspNetCore.Mvc;
using NetFlow.Domain.Common.Pagination;
using NetFlow.Domain.Identity;
using NetFlow.ReadModel.ServiceFormDocuments;

namespace NetFlow.Api.Controllers
{
    [Route("api/service-form-documents")]
    [ApiController]
    public class ServiceFormDocumentController : ControllerBase
    {
        private readonly ServiceFormDocumentReadService _read;
        protected readonly CurrentUser _current;

        public ServiceFormDocumentController(ServiceFormDocumentReadService read, CurrentUser current)
        {
            _read = read;
            _current = current;
        }

        [HttpGet]
        public async Task<IActionResult> List([FromQuery] int serviceFormId, [FromQuery] PagedRequest pagedRequest)
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

using Microsoft.AspNetCore.Mvc;
using NetFlow.Application.ServiceDetails;
using NetFlow.Domain.Common.Pagination;
using NetFlow.Domain.Identity;
using NetFlow.ReadModel.ServiceDetails;

namespace NetFlow.Api.Controllers
{
    
    [ApiController]
    [Route("api/service-details")]
    public class ServiceDetailController : ControllerBase
    {
        private readonly ServiceDetailReadService _read;
        private readonly ServiceDetailWriteService _write;
        protected readonly CurrentUser _current;

        public ServiceDetailController(ServiceDetailReadService read, CurrentUser current, ServiceDetailWriteService write)
        {
            _read = read;
            _current = current;
            _write = write;
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


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateServiceDetailRequest request)
        {
            if (_current.User == null)
            {
                return NotFound();
            }

            var id = await _write.CreateAsync(_current.User.Id.Value, request);

            return CreatedAtAction(
                nameof(Get),
                new { id },
                null);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] EditServiceDetailRequest request)
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

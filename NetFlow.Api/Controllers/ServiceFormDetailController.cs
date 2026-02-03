using Microsoft.AspNetCore.Mvc;
using NetFlow.Application.ServiceFormDetails;
using NetFlow.Application.ServiceForms;
using NetFlow.Domain.Common.Pagination;
using NetFlow.Domain.Identity;
using NetFlow.ReadModel.ServiceFormDetails;

namespace NetFlow.Api.Controllers
{
    
    [ApiController]
    [Route("api/service-form-details")]
    public class ServiceFormDetailController : ControllerBase
    {
        private readonly ServiceFormDetailReadService _read;
        private readonly ServiceFormDetailWriteService _write;
        protected readonly CurrentUser _current;

        public ServiceFormDetailController(ServiceFormDetailReadService read, CurrentUser current, ServiceFormDetailWriteService write)
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
        public async Task<IActionResult> Create([FromBody] CreateServiceFormDetailRequest request)
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
        public async Task<IActionResult> Update([FromBody] EditServiceFormDetailRequest request)
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

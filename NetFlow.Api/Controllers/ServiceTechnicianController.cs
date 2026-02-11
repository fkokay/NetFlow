using Microsoft.AspNetCore.Mvc;
using NetFlow.Application.ServiceForms;
using NetFlow.Application.ServiceTechnicians;
using NetFlow.Domain.Common.Pagination;
using NetFlow.Domain.Identity;
using NetFlow.ReadModel.ServiceTechnicians;

namespace NetFlow.Api.Controllers
{
    [Route("api/service-technicians")]
    [ApiController]
    public class ServiceTechnicianController : ControllerBase
    {
        private readonly ServiceTechnicianReadService _read;
        private readonly ServiceTechnicianWriteService _write;
        private readonly ServiceFormWriteService _writeService;
        protected readonly CurrentUser _current;
        public ServiceTechnicianController(ServiceTechnicianReadService read, CurrentUser current, ServiceTechnicianWriteService write, ServiceFormWriteService writeService)
        {
            _read = read;
            _current = current;
            _write = write;
            _writeService = writeService;
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
        public async Task<IActionResult> Create([FromBody] CreateServiceTechnicianRequest request)
        {
            if (_current.User == null)
            {
                return NotFound();
            }
            var id = await _write.CreateAsync(_current.User.Id.Value,request);

            await _writeService.EditServiceFormTechnician(new EditServiceFormTechnicianVRequest
            {
                ServiceFormId = request.ServiceFormId,
                CreatedBy = _current.User.Id.Value,
                IsTechnicianAssigned = true
            });

            return CreatedAtAction(
                nameof(Get),
                new { id },
                null);
        }
    }
}

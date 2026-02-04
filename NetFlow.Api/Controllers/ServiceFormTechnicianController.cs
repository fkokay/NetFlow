using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetFlow.Application.Firms;
using NetFlow.Application.Guarantees;
using NetFlow.Application.ServiceForms;
using NetFlow.Application.ServiceFormTechnicians;
using NetFlow.Domain.Common.Pagination;
using NetFlow.Domain.Identity;
using NetFlow.ReadModel.Firms;
using NetFlow.ReadModel.ServiceFormTechnicians;

namespace NetFlow.Api.Controllers
{
    [Route("api/service-form-technicians")]
    [ApiController]
    public class ServiceFormTechnicianController : ControllerBase
    {
        private readonly ServiceFormTechnicianReadService _read;
        private readonly ServiceFormTechnicianWriteService _write;
        private readonly ServiceFormWriteService _writeService;
        protected readonly CurrentUser _current;
        public ServiceFormTechnicianController(ServiceFormTechnicianReadService read, CurrentUser current, ServiceFormTechnicianWriteService write, ServiceFormWriteService writeService)
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
        public async Task<IActionResult> Create([FromBody] CreateServiceFormTechnicianRequest request)
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

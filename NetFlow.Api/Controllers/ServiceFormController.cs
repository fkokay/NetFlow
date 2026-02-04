using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetFlow.Application.Guarantees;
using NetFlow.Application.Roles;
using NetFlow.Application.ServiceFormHistories;
using NetFlow.Application.ServiceForms;
using NetFlow.Domain.Common.Pagination;
using NetFlow.Domain.Enums;
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
        private readonly ServiceFormWriteService _write;
        private readonly ServiceFormHistoryWriteService _historyWrite;
        protected readonly CurrentUser _current;

        public ServiceFormController(ServiceFormReadService read, CurrentUser current, ServiceFormWriteService write, ServiceFormHistoryWriteService historyWrite)
        {
            _read = read;
            _current = current;
            _write = write;
            _historyWrite = historyWrite;
        }

        [HttpGet("list")]
        public async Task<IActionResult> List([FromQuery] PagedRequest pagedRequest)
        {
            if (_current.User == null)
            {
                return NotFound();
            }

            return Ok(await _read.ListAsync(_current.User.Id.Value, pagedRequest));
        }

        [HttpGet("open")]
        public async Task<IActionResult> Open([FromQuery] PagedRequest pagedRequest)
        {
            if (_current.User == null)
            {
                return NotFound();
            }

            return Ok(await _read.ListAsync(_current.User.Id.Value, pagedRequest, open: true));
        }
        [HttpGet("closed")]
        public async Task<IActionResult> Closed([FromQuery] PagedRequest pagedRequest)
        {
            if (_current.User == null)
            {
                return NotFound();
            }

            return Ok(await _read.ListAsync(_current.User.Id.Value, pagedRequest, closed: true));
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var row = await _read.GetAsync(id);
            return row is null ? NotFound() : Ok(row);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateServiceFormRequest request)
        {
            if (_current.User == null)
            {
                return NotFound();
            }

            var id = await _write.CreateAsync(_current.User.Id.Value, request);
            var historyRequest = new CreateServiceFormHistoryRequest
            {
                ServiceFormId = id,
                ActionType = ServiceActionType.Created,
                NewStatus = ServiceStatus.Draft,
                NewPersonnelId = request.AssignedPersonnelId,
                Description = "Servis formu oluşturuldu",
                ActionBy = _current.User.Id.Value,
                ActionAt = DateTime.UtcNow,
                Source = "Web"
            };

            await _historyWrite.CreateAsync(_current.User.Id.Value, historyRequest);
            return CreatedAtAction(
                nameof(Get),
                new { id },
                null);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] EditServiceFormRequest request)
        {
            var id = await _write.EditAsync(request);
            return CreatedAtAction(
                nameof(Get),
                new { id },
                null);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using NetFlow.Application.MaterialRequests;
using NetFlow.Application.Modules;
using NetFlow.Application.Users;
using NetFlow.Domain.Common.Pagination;
using NetFlow.Domain.Identity;
using NetFlow.ReadModel.Requests;
using NetFlow.ReadModel.Users;

namespace NetFlow.Api.Controllers
{
    [ApiController]
    [Route("api/material-requests")]
    public class MaterialRequestsController : ControllerBase
    {
        private readonly CurrentUser _current;
        private readonly MaterialRequestReadService _read;
        private readonly MaterialRequestWriteService _write;

        public MaterialRequestsController(CurrentUser current, MaterialRequestReadService read, MaterialRequestWriteService write)
        {
            _current = current;
            _read = read;
            _write = write;
        }

        [HttpGet]
        public async Task<IActionResult> List([FromQuery] PagedRequest pagedRequest) => Ok(await _read.ListAsync(_current.User.Id.Value,_current.User.Firm.Id, pagedRequest));

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var row = await _read.GetAsync(id);
            return row is null ? NotFound() : Ok(row);
        }

        [HttpGet("open")]
        public async Task<IActionResult> Open([FromQuery] PagedRequest pagedRequest)
        {
            if (_current.User == null)
            {
                return NotFound();
            }

            return Ok(await _read.ListAsync(_current.User.Id.Value,_current.User.Firm.Id, pagedRequest, open: true));
        }
        [HttpGet("closed")]
        public async Task<IActionResult> Closed([FromQuery] PagedRequest pagedRequest)
        {
            if (_current.User == null)
            {
                return NotFound();
            }

            return Ok(await _read.ListAsync(_current.User.Id.Value,_current.User.Firm.Id, pagedRequest, closed: true));
        }
        [HttpGet("My")]
        public async Task<IActionResult> My([FromQuery] PagedRequest pagedRequest)
        {
            if (_current.User == null)
            {
                return NotFound();
            }

            return Ok(await _read.ListAsync(_current.User.Id.Value,_current.User.Firm.Id, pagedRequest, my: true));
        }
        [HttpGet("Waiting")]
        public async Task<IActionResult> Waiting([FromQuery] PagedRequest pagedRequest)
        {
            if (_current.User == null)
            {
                return NotFound();
            }

            return Ok(await _read.ListAsync(_current.User.Id.Value,_current.User.Firm.Id, pagedRequest, waiting: true));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMaterialRequest request)
        {
            var id = await _write.CreateAsync(request);

            return CreatedAtAction(
                nameof(Get),
                new { id },
                null);
        }

        [HttpPut("rejection")]
        public async Task<IActionResult> Rejection([FromBody] RejectionMaterialRequest request)
        {
            var id = await _write.RejectionAsync(request);

            return CreatedAtAction(
                nameof(Get),
                new { id },
                null);
        }

        [HttpPut("approved/{materialId:int}")]
        public async Task<IActionResult> Approved(int materialId)
        {
            if (_current.User == null)
            {
                return NotFound();
            }

            var id = await _write.ApprovedAsync(_current.User.Id.Value, materialId);

            return CreatedAtAction(
                nameof(Get),
                new { id },
                null);
        }
    }
}

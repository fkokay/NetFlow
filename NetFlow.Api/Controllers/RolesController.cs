using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetFlow.Application.Firms;
using NetFlow.Application.Roles;
using NetFlow.Domain.Common.Pagination;
using NetFlow.Domain.Identity;
using NetFlow.ReadModel.Roles;
using NetFlow.ReadModel.Tenders;

namespace NetFlow.Api.Controllers
{
    [ApiController]
    [Route("api/roles")]
    [Authorize]
    public class RolesController : ControllerBase
    {
        protected readonly CurrentUser _current;
        private readonly RoleReadService _read;
        private readonly RoleWriteService _write;
        public RolesController(CurrentUser current, RoleReadService read, RoleWriteService write)
        {
            _current = current;
            _read = read;
            _write = write;
        }

        // GET api/roles
        [HttpGet]
        public async Task<IActionResult> List([FromQuery] PagedRequest pagedRequest)
        {
            if (_current.User == null)
            {
                return NotFound();
            }
            return Ok(await _read.ListAsync(_current.User.Firm.Id, pagedRequest));
        }
        [HttpGet("rolelist")]
        public async Task<IActionResult> List() => Ok(await _read.GetRoleListAsync());
        // GET api/roles/10
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var row = await _read.GetAsync(id);
            return row is null ? NotFound() : Ok(row);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRoleRequest request)
        {
            var id = await _write.CreateAsync(request);

            return CreatedAtAction(
                nameof(Get),
                new { id },
                null);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] EditRoleRequest request)
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

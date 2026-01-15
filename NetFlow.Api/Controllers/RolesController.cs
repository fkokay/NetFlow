using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetFlow.Application.Common.Pagination;
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

        public RolesController(CurrentUser current, RoleReadService read)
        {
            _current = current;
            _read = read;
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

        // GET api/roles/10
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var row = await _read.GetAsync(id);
            return row is null ? NotFound() : Ok(row);
        }
    }
}

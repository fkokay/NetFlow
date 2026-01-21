using Microsoft.AspNetCore.Mvc;
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

        public MaterialRequestsController(CurrentUser current, MaterialRequestReadService read)
        {
            _current = current;
            _read = read;
        }

        [HttpGet]
        public async Task<IActionResult> List([FromQuery] PagedRequest pagedRequest) => Ok(await _read.ListAsync(_current.User.Firm.Id, pagedRequest));

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var row = await _read.GetAsync(id);
            return row is null ? NotFound() : Ok(row);
        }

    }
}

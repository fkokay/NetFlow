using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetFlow.Application.Common.Pagination;
using NetFlow.Application.Users;
using NetFlow.ReadModel.Users;

namespace NetFlow.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {

        private readonly UserReadService _read;
        private readonly UserWriteService _write;

        public UsersController(UserReadService read, UserWriteService write)
        {
            _read = read;
            _write = write;
        }

        [HttpGet]
        public async Task<IActionResult> List([FromQuery] PagedRequest pagedRequest) => Ok(await _read.ListAsync(pagedRequest));

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var row = await _read.GetAsync(id);
            return row is null ? NotFound() : Ok(row);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
        {
            var id = await _write.CreateAsync(request);

            return CreatedAtAction(
                nameof(Get),
                new { id },
                null);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] EditUserRequest request)
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

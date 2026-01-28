using Microsoft.AspNetCore.Mvc;
using NetFlow.Application.Guarantees;
using NetFlow.Application.Personnels;
using NetFlow.Domain.Common.Pagination;
using NetFlow.Domain.Identity;
using NetFlow.ReadModel.Personnel;

namespace NetFlow.Api.Controllers
{
    [Route("api/personnels")]
    [ApiController]
    public class PersonnelsController : ControllerBase
    {
        private readonly PersonnelReadService _read;
        private readonly PersonnelWriteService _write;
        protected readonly CurrentUser _current;

        public PersonnelsController(PersonnelReadService read, CurrentUser current, PersonnelWriteService write)
        {
            _read = read;
            _current = current;
            _write = write;
        }
        [HttpGet("list")]
        public async Task<IActionResult> List([FromQuery] PagedRequest pagedRequest)
        {
            return Ok(await _read.ListAsync(pagedRequest));
        }
        [HttpGet("active")]
        public async Task<IActionResult> Active([FromQuery] PagedRequest pagedRequest)
        {
            return Ok(await _read.ListAsync(pagedRequest, isActive: true));
        }

        [HttpGet("terminate")]
        public async Task<IActionResult> Terminate([FromQuery] PagedRequest pagedRequest)
        {
            return Ok(await _read.ListAsync(pagedRequest,isTerminate:true));
        }
        
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var row = await _read.GetAsync(id);
            return row is null ? NotFound() : Ok(row);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePersonnelRequest request)
        {
            var id = await _write.CreateAsync(request);

            return CreatedAtAction(
                nameof(Get),
                new { id },
                null);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] EditPersonnelRequest request)
        {
            var id = await _write.EditAsync(request);
            return CreatedAtAction(
                nameof(Get),
                new { id },
                null);
        }
        [HttpPut("terminate")]
        public async Task<IActionResult> Update([FromBody] TerminatePersonnelRequest request)
        {
            var id = await _write.TerminateAsync(request);
            return CreatedAtAction(
                nameof(Get),
                new { id },
                null);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _write.DeleteAsync(id);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }
    }
}

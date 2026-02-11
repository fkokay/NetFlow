using Microsoft.AspNetCore.Mvc;
using NetFlow.Application.TenderAuthorities;
using NetFlow.Application.Tenders;
using NetFlow.Domain.Common;
using NetFlow.Domain.Common.Pagination;

[ApiController]
[Route("api/tender-authorities")]
public class TenderAuthorityController : ControllerBase
{
    private readonly TenderAuthorityReadService _read;
    private readonly TenderAuthorityWriteService _write;

    public TenderAuthorityController(TenderAuthorityReadService read, TenderAuthorityWriteService write)
    {
        _read = read;
        _write = write;
    }

    // GET api/tender-authorities?tenderId=5
    [HttpGet]
    public async Task<IActionResult> List([FromQuery] int tenderId, [FromQuery] PagedRequest pagedRequest)=> Ok(await _read.ListAsync(tenderId, pagedRequest));

    // GET api/tender-authorities/12
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var row = await _read.GetAsync(id);
        return row is null ? NotFound() : Ok(row);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTenderAuthorityRequest request)
    {
        var id = await _write.CreateAsync(request);
        return CreatedAtAction(
            nameof(Get),
            new { id },
            null);
    }
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] EditTenderAuthorityRequest request)
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

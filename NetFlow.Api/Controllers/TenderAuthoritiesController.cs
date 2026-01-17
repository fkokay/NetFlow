using Microsoft.AspNetCore.Mvc;
using NetFlow.Domain.Common;
using NetFlow.Domain.Common.Pagination;

[ApiController]
[Route("api/tender-authorities")]
public class TenderAuthorityController : ControllerBase
{
    private readonly TenderAuthorityReadService _read;

    public TenderAuthorityController(TenderAuthorityReadService read)
    {
        _read = read;
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
}

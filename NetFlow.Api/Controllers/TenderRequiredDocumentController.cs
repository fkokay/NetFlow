using Microsoft.AspNetCore.Mvc;
using NetFlow.Domain.Common;
using NetFlow.Domain.Common.Pagination;
using NetFlow.ReadModel.TenderDocuments;

namespace NetFlow.Api.Controllers
{
    [ApiController]
    [Route("api/tender-documents")]
    public class TenderRequiredDocumentController : ControllerBase
    {
        private readonly TenderRequiredDocumentReadService _read;


        public TenderRequiredDocumentController(
            TenderRequiredDocumentReadService read)
        {
            _read = read;
        }

        // GET api/tender-documents?tenderId=5
        [HttpGet]
        public async Task<IActionResult> List([FromQuery] int tenderId, [FromQuery] PagedRequest pagedRequest) => Ok(await _read.ListAsync(tenderId, pagedRequest));


    }
}

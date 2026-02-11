using Microsoft.AspNetCore.Mvc;
using NetFlow.Application.MaterialRequestItems;
using NetFlow.Application.MaterialRequests;
using NetFlow.Application.TenderCapexes;
using NetFlow.Application.TenderOpexes;
using NetFlow.Domain.Common.Pagination;
using NetFlow.Domain.Identity;

namespace NetFlow.Api.Controllers
{
    [ApiController]
    [Route("api/tender-capex")]
    public class TenderCapexController : ControllerBase
    {
        private readonly TenderCapexReadService _read;
        private readonly TenderCapexWriterService _write;
        private readonly MaterialRequestWriteService _materialRequestWrite;
        private readonly MaterialRequestItemWriteService _materialRequestItemWrite;
        private readonly CurrentUser _current;

        public TenderCapexController(TenderCapexReadService read, TenderCapexWriterService write, MaterialRequestWriteService materialRequestWrite, MaterialRequestItemWriteService materialRequestItemWrite, CurrentUser current)
        {
            _read = read;
            _write = write;
            _materialRequestWrite = materialRequestWrite;
            _materialRequestItemWrite = materialRequestItemWrite;
            _current = current;
        }

        // GET api/tender-capex?tenderId=5
        [HttpGet]
        public async Task<IActionResult> List([FromQuery] int tenderId, [FromQuery] PagedRequest pagedRequest) => Ok(await _read.ListAsync(tenderId, pagedRequest));


        // GET api/tender-capex/12
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var row = await _read.GetAsync(id);
            return row is null ? NotFound() : Ok(row);
        }



        [HttpPost("create-material-request")]
        public async Task<IActionResult> CreateMaterialRequest([FromBody] TenderCapexCreateMaterialRequest request)
        {
            if (_current.User == null)
            {
                return NotFound();
            }

            int materialRequestId = await _materialRequestWrite.CreateAsync(_current, new CreateMaterialRequest()
            {
                Description = request.Description,
                Priority = request.Priority,
                RequestedDepartment = request.RequestedDepartment,
                RequiredDate = request.RequiredDate,
                RequestType = request.RequestType,
                SourceType = request.SourceType
            });

            int materialRequestItemId = await _materialRequestItemWrite.CreateAsync(new CreateMaterialRequestItemRequest()
            {
                MaterialRequestId = materialRequestId,
                StockCode= request.StockCode,
                RequestedQuantity = request.RequestedQuantity,
                FulfilledQuantity = request.FulfilledQuantity,
                Unit = request.Unit,
                WarehouseCode = request.WarehouseCode,
                AlternateItemCode = request.AlternateItemCode,
                Status = request.Status,
                FulfillmentType = request.FulfillmentType
            });

            var status = await _write.UpdateMaterialRequest(request, materialRequestId, materialRequestItemId);
            return Ok(status);
        }
    }
}

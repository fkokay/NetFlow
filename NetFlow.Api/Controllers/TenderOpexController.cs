using Microsoft.AspNetCore.Mvc;
using NetFlow.Application.MaterialRequestItems;
using NetFlow.Application.MaterialRequests;
using NetFlow.Application.TenderOpexes;
using NetFlow.Domain.Common;
using NetFlow.Domain.Common.Pagination;
using NetFlow.Domain.Identity;
using NetFlow.ReadModel.TenderOpex;

namespace NetFlow.Api.Controllers
{
    [ApiController]
    [Route("api/tender-opex")]
    public class TenderOpexController : ControllerBase
    {
        private readonly TenderOpexReadService _read;
        private readonly TenderOpexWriterService _write;
        private readonly MaterialRequestWriteService _materialRequestWrite;
        private readonly MaterialRequestItemWriteService _materialRequestItemWrite;
        private readonly CurrentUser _current;

        public TenderOpexController(TenderOpexReadService read, TenderOpexWriterService writer, MaterialRequestWriteService materialRequestWriteService, MaterialRequestItemWriteService materialRequestItemWriteService, CurrentUser current)
        {
            _read = read;
            _write = writer;
            _materialRequestWrite = materialRequestWriteService;
            _materialRequestItemWrite = materialRequestItemWriteService;
            _current = current;
        }

        // GET api/tender-opex?tenderId=5
        [HttpGet]
        public async Task<IActionResult> List([FromQuery] int tenderId, [FromQuery] PagedRequest pagedRequest) => Ok(await _read.ListAsync(tenderId, pagedRequest));

        // GET api/tender-opex/12
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var row = await _read.GetAsync(id);
            return row is null ? NotFound() : Ok(row);
        }

        [HttpPost("create-material-request")]
        public async Task<IActionResult> CreateMaterialRequest([FromBody] TenderOpexCreateMaterialRequest request)
        {
            if (_current.User == null)
            {
                return NotFound();
            }

            int materialRequestId = await _materialRequestWrite.CreateAsync(_current.User.Id.Value, new CreateMaterialRequest()
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
                ItemCode = request.ItemCode,
                ItemName = request.ItemName,
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

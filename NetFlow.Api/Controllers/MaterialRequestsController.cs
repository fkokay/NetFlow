using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using NetFlow.Application.MaterialRequestHistories;
using NetFlow.Application.MaterialRequests;
using NetFlow.Application.Modules;
using NetFlow.Application.Users;
using NetFlow.Domain.Common.Pagination;
using NetFlow.Domain.Entities;
using NetFlow.Domain.Enums;
using NetFlow.Domain.Identity;
using NetFlow.Domain.Tenders;
using NetFlow.ReadModel.Firms;
using NetFlow.ReadModel.MaterialRequestItems;
using NetFlow.ReadModel.Requests;
using NetFlow.ReadModel.Users;
using NetOpenX.Rest.Client;
using NetOpenX.Rest.Client.BLL;
using NetOpenX.Rest.Client.Model;
using NetOpenX.Rest.Client.Model.NetOpenX;

namespace NetFlow.Api.Controllers
{
    [ApiController]
    [Route("api/material-requests")]
    public class MaterialRequestsController : ControllerBase
    {
        private readonly CurrentUser _current;
        private readonly MaterialRequestReadService _read;
        private readonly MaterialRequestItemReadService _itemRead;
        private readonly MaterialRequestWriteService _write;
        private readonly MaterialRequestHistoryWriteService _historyWrite;
        private readonly FirmReadService _firmRead;

        public MaterialRequestsController(CurrentUser current, MaterialRequestReadService read, MaterialRequestItemReadService itemRead, MaterialRequestWriteService write, MaterialRequestHistoryWriteService historyWrite, FirmReadService firmRead)
        {
            _current = current;
            _read = read;
            _itemRead = itemRead;
            _write = write;
            _historyWrite = historyWrite;
            _firmRead = firmRead;
        }

        [HttpGet]
        public async Task<IActionResult> List([FromQuery] PagedRequest pagedRequest)
        {
            if (_current.User == null)
            {
                return NotFound();
            }

            return Ok(await _read.ListAsync(_current.User.Id.Value, _current.User.Firm.Id, pagedRequest));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var row = await _read.GetAsync(id);
            return row is null ? NotFound() : Ok(row);
        }

        [HttpGet("open")]
        public async Task<IActionResult> Open([FromQuery] PagedRequest pagedRequest)
        {
            if (_current.User == null)
            {
                return NotFound();
            }

            return Ok(await _read.ListAsync(_current.User.Id.Value, _current.User.Firm.Id, pagedRequest, open: true));
        }
        [HttpGet("closed")]
        public async Task<IActionResult> Closed([FromQuery] PagedRequest pagedRequest)
        {
            if (_current.User == null)
            {
                return NotFound();
            }

            return Ok(await _read.ListAsync(_current.User.Id.Value, _current.User.Firm.Id, pagedRequest, closed: true));
        }
        [HttpGet("My")]
        public async Task<IActionResult> My([FromQuery] PagedRequest pagedRequest)
        {
            if (_current.User == null)
            {
                return NotFound();
            }

            return Ok(await _read.ListAsync(_current.User.Id.Value, _current.User.Firm.Id, pagedRequest, my: true));
        }
        [HttpGet("Waiting")]
        public async Task<IActionResult> Waiting([FromQuery] PagedRequest pagedRequest)
        {
            if (_current.User == null)
            {
                return NotFound();
            }

            return Ok(await _read.ListAsync(_current.User.Id.Value, _current.User.Firm.Id, pagedRequest, waiting: true));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMaterialRequest request)
        {
            if (_current.User == null)
            {
                return NotFound();
            }
            var id = await _write.CreateAsync(_current.User.Id.Value, request);


            var materialRequstHistory = new CreateMaterialRequestHistoryRequest();
            materialRequstHistory.Action = MaterialRequestHistoryAction.Created;
            materialRequstHistory.ActionDate = DateTime.UtcNow;
            materialRequstHistory.MaterialRequestId = id;
            materialRequstHistory.ActionByUserId = _current.User.Id.Value;
            materialRequstHistory.Notes = "Talep Oluşturuldu";
            var historyId=await _historyWrite.CreateAsync(materialRequstHistory);

            return CreatedAtAction(
                nameof(Get),
                new { id },
                null);
        }

        [HttpPut("rejection")]
        public async Task<IActionResult> Rejection([FromBody] RejectionMaterialRequest request)
        {
            if (_current.User == null)
            {
                return NotFound();
            }
            var id = await _write.RejectionAsync(_current.User.Id.Value, request);


            var materialRequstHistory = new CreateMaterialRequestHistoryRequest();
            materialRequstHistory.Action = MaterialRequestHistoryAction.Rejected;
            materialRequstHistory.ActionDate = DateTime.UtcNow;
            materialRequstHistory.MaterialRequestId = id;
            materialRequstHistory.ActionByUserId = _current.User.Id.Value;
            materialRequstHistory.Notes = "Talep Reddedildi";
            var historyId = await _historyWrite.CreateAsync(materialRequstHistory);


            return CreatedAtAction(
                nameof(Get),
                new { id },
                null);
        }

        [HttpPut("approved/{materialId:int}")]
        public async Task<IActionResult> Approved(int materialId)
        {
            if (_current.User == null)
            {
                return NotFound();
            }

            var id = await _write.ApprovedAsync(_current.User.Id.Value, materialId);

            var materialRequstHistory = new CreateMaterialRequestHistoryRequest();
            materialRequstHistory.Action = MaterialRequestHistoryAction.Approved;
            materialRequstHistory.ActionDate = DateTime.UtcNow;
            materialRequstHistory.MaterialRequestId = id;
            materialRequstHistory.ActionByUserId = _current.User.Id.Value;
            materialRequstHistory.Notes = "Talep Onaylandı";
            var historyId = await _historyWrite.CreateAsync(materialRequstHistory);

            return CreatedAtAction(
                nameof(Get),
                new { id },
                null);
        }


        [HttpPut("fulfill-items")]
        public async Task<IActionResult> FulfillItems([FromBody] FulfillmentRequest requests)
        {
            if (_current.User == null)
            {
                return NotFound();
            }

            var firm = await _firmRead.GetAsync(_current.User.Firm.Id);
            if (firm == null)
            {
                return NotFound();
            }

            var materialRequest = await _read.GetAsync(requests.Id);
            if (materialRequest == null)
            {
                return NotFound();
            }

            var items = await _itemRead.ListAsync(requests.Id,new PagedRequest()
            {
                Skip = 0,
                Take = int.MaxValue
            });

            oAuth2 auth2 = new oAuth2(firm.NetsisRestApiUrl);
            var token = await auth2.LoginAsync(new JLogin()
            {
                BranchCode = 0,
                DbName = firm.NetsisDbName,
                DbPassword = "",
                DbType = JNVTTipi.vtMSSQL,
                DbUser = "TEMELSET",
                NetsisUser = firm.NetsisUser,
                NetsisPassword = firm.NetsisPassword
            });

            ItemSlipsManager itemSlipsManager = new ItemSlipsManager(auth2);
  

            var purchaseCustomers = (items.Data as List<MaterialRequestItemDto>).Select(x => x.PurchaseCustomerCode).Distinct().ToList();
            foreach (var customer in purchaseCustomers)
            {

                var slipItems = (items.Data as List<MaterialRequestItemDto>).Where(m => m.PurchaseCustomerCode == customer).ToList();

                ItemSlips slips = new ItemSlips();
                slips.FaturaTip = NetOpenX.Rest.Client.Model.Enums.JTFaturaTip.ftASip;
                slips.SeriliHesapla = false;
                slips.KayitliNumaraOtomatikGuncellensin = true;
                slips.FatUst = new ItemSlipsHeader();
                slips.FatUst.TIPI = NetOpenX.Rest.Client.Model.Enums.JTFaturaTipi.ft_Acik;
                slips.FatUst.CariKod = customer;
                slips.FatUst.Tarih = DateTime.Now;
                slips.FatUst.FIYATTARIHI = DateTime.Now;
                slips.FatUst.SIPARIS_TEST = DateTime.Now;
                slips.FatUst.KDV_DAHILMI = true;
                slips.FatUst.PLA_KODU = "TEST";
                slips.FatUst.KOD1 = "L";
                slips.FatUst.Aciklama = "";


                slips.Kalems = new List<ItemSlipLines>();
                foreach (var item in slipItems)
                {
                    slips.Kalems.Add(new ItemSlipLines
                    {
                        StokKodu = item.ItemCode,
                        STra_GCMIK = Convert.ToDouble(item.FulfilledQuantity),
                        STra_BF = Convert.ToDouble(item.Price),
                        DEPO_KODU = 1,
                    });
                }

                var resultNetsisOrder = itemSlipsManager.PostInternal(slips);
                if (!resultNetsisOrder.IsSuccessful)
                {
                    throw new Exception(resultNetsisOrder.Message);
                }
            }

            var ids = await _write.FulFillmentAsync(_current.User.Id.Value, requests);

          
            var materialRequstHistory = new CreateMaterialRequestHistoryRequest                                                     ();
            materialRequstHistory.Action = MaterialRequestHistoryAction.Fulfilled;
            materialRequstHistory.ActionDate = DateTime.UtcNow;
            materialRequstHistory.MaterialRequestId = requests.Id;
            materialRequstHistory.ActionByUserId = _current.User.Id.Value;
            materialRequstHistory.Notes = "Talep Karşılandı";
            var historyId = await _historyWrite.CreateAsync(materialRequstHistory);

            return Ok(new
            {
                ids.Count,
                Ids = ids
            });
        }
    }
}

using NetFlow.Application.Common.Interfaces;
using NetFlow.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Application.MaterialRequestItems
{
    public class MaterialRequestItemWriteService
    {
        private readonly INetFlowDbContext _db;

        public MaterialRequestItemWriteService(INetFlowDbContext db)
        {
            _db = db;
        }

        public async Task<int> CreateAsync(CreateMaterialRequestItem createMaterialRequestItem)
        {
            var materialRequestItem = new Domain.Entities.MaterialRequestItemEntity();
            materialRequestItem.MaterialRequestId = createMaterialRequestItem.MaterialRequestId;
            materialRequestItem.ItemCode = createMaterialRequestItem.ItemCode;
            materialRequestItem.ItemName = createMaterialRequestItem.ItemName;
            materialRequestItem.RequestedQuantity = createMaterialRequestItem.RequestedQuantity;
            materialRequestItem.FulfilledQuantity = createMaterialRequestItem.FulfilledQuantity;
            materialRequestItem.Unit = createMaterialRequestItem.Unit;
            materialRequestItem.WarehouseCode = createMaterialRequestItem.WarehouseCode;
            materialRequestItem.AlternateItemCode = createMaterialRequestItem.AlternateItemCode;
            materialRequestItem.Status = (MaterialRequestItemStatus)createMaterialRequestItem.Status;
            materialRequestItem.CreatedAt = createMaterialRequestItem.CreatedAt;
            materialRequestItem.FulfillmentType = (MaterialRequestItemFulfillmentType)createMaterialRequestItem.FulfillmentType;

            _db.MaterialRequestItems.Add(materialRequestItem);
            await _db.SaveChangesAsync();
            return materialRequestItem.Id;

        }
    }
}

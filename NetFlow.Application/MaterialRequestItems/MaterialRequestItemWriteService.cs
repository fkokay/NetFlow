using Microsoft.EntityFrameworkCore;
using NetFlow.Application.Common.Interfaces;
using NetFlow.Application.Guarantees;
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

        public async Task<int> CreateAsync(CreateMaterialRequestItemRequest createMaterialRequestItem)
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
            materialRequestItem.Price = createMaterialRequestItem.Price;
            _db.MaterialRequestItems.Add(materialRequestItem);
            await _db.SaveChangesAsync();
            return materialRequestItem.Id;

        }
        public async Task<int> EditAsync(EditMaterialRequestItemRequest request)
        {
            var materialRequestItem = await _db.MaterialRequestItems.FirstOrDefaultAsync(x => x.Id == request.Id) ?? throw new Exception("Kalem bulunamadı");

            materialRequestItem.ItemCode= request.ItemCode;
            materialRequestItem.ItemName= request.ItemName;
            materialRequestItem.RequestedQuantity= request.RequestedQuantity;
            materialRequestItem.Price= request.Price;
            materialRequestItem.Unit= request.Unit;
            materialRequestItem.WarehouseCode= request.WarehouseCode;
            materialRequestItem.AlternateItemCode= request.AlternateItemCode;

            await _db.SaveChangesAsync();
            return materialRequestItem.Id;
        }
        public async Task DeleteAsync(int id)
        {
            var materialRequestItem = await _db.MaterialRequestItems.FirstOrDefaultAsync(x => x.Id == id);
            _db.MaterialRequestItems.Remove(materialRequestItem);
            await _db.SaveChangesAsync();
        }
    }
}

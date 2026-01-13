using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Domain.Shipping
{
    public sealed class ShipmentOrder
    {
        public string OrderNo { get; }
        public string StockCode { get; }
        public decimal Quantity { get; }
        public string Warehouse { get; }
        public decimal AvailableStock { get; }

        private ShipmentOrder(
            string orderNo,
            string stockCode,
            decimal quantity,
            string warehouse,
            decimal availableStock)
        {
            OrderNo = orderNo;
            StockCode = stockCode;
            Quantity = quantity;
            Warehouse = warehouse;
            AvailableStock = availableStock;
        }

        public static ShipmentOrder Create(
            string orderNo,
            string stockCode,
            decimal quantity,
            string warehouse,
            decimal availableStock)
        {
            if (string.IsNullOrWhiteSpace(orderNo))
                throw new InvalidShipmentOrderNumberException();

            if (string.IsNullOrWhiteSpace(stockCode))
                throw new InvalidShipmentStockCodeException();

            if (quantity <= 0)
                throw new InvalidShipmentQuantityException();

            return new ShipmentOrder(
                orderNo,
                stockCode,
                quantity,
                warehouse,
                availableStock
            );
        }

        public bool CanBeShipped()
            => Quantity <= AvailableStock;
    }
}

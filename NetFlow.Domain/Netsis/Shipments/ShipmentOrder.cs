using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Domain.Netsis.Shipments
{
    public sealed class ShipmentOrder
    {
        public int Id { get; }
        public string OrderNo { get; }
        public string CustomerCode { get; }
        public string CustomerName { get; }
        public string StockCode { get; }
        public string StockName { get; }
        public decimal Quantity { get; }
        public string Warehouse { get; }
        public decimal AvailableStock { get; }

        private ShipmentOrder(
            int id,
            string orderNo,
            string customerCode,
            string customerName,
            string stockCode,
            string stockName,
            decimal quantity,
            string warehouse,
            decimal availableStock)
        {
            Id = id;
            OrderNo = orderNo;
            CustomerCode = customerCode;
            CustomerName = customerName;
            StockCode = stockCode;
            StockName = stockName;
            Quantity = quantity;
            Warehouse = warehouse;
            AvailableStock = availableStock;
        }

        public static ShipmentOrder Create(
            int id,
            string orderNo,
            string customerCode,
            string customerName,
            string stockCode,
            string stockName,
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
                id,
                orderNo,
                customerCode,
                customerName,
                stockCode,
                stockName,
                quantity,
                warehouse,
                availableStock
            );
        }

        public bool CanBeShipped()
            => Quantity <= AvailableStock;
    }
}

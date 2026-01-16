using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Domain.Netsis.Orders
{
    public class Order
    {
        public short BranchCode { get; set; }
        public string OrderType { get; set; } = string.Empty;
        public string OrderNumber { get; set; } = string.Empty;
        public string CustomerCode { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public short? OrderStatus { get; set; }
        public string? Description { get; set; }
        public double NetTotal { get; set; }

        public Order(short branchCode, string orderType, string orderNumber, string customerCode, string customerName, DateTime orderDate, short? orderStatus, string? description, double netTotal)
        {
            BranchCode = branchCode;
            OrderType = orderType;
            OrderNumber = orderNumber;
            CustomerCode = customerCode;
            CustomerName = customerName;
            OrderDate = orderDate;
            OrderStatus = orderStatus;
            Description = description;
            NetTotal = netTotal;
        }

        public static Order Create(
            short branchCode,
            string orderType,
            string orderNumber,
            string customerCode,
            string customerName,
            DateTime orderDate,
            short? orderStatus,
            string? description,
            double netTotal)
        {
            return new Order(
                branchCode,
                orderType,
                orderNumber,
                customerCode,
                customerName,
                orderDate,
                orderStatus,
                description,
                netTotal);
        }
    }
}

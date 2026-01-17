using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Domain.Netsis.Orders
{
    public class Order
    {
        public short BranchCode { get; }
        public string OrderType { get;  } 
        public string OrderNumber { get;  } 
        public string CustomerCode { get;  } 
        public string CustomerName { get;  } 
        public DateTime OrderDate { get;  }
        public short? OrderStatus { get;  }
        public string? Description { get;  }
        public double NetTotal { get;  }

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

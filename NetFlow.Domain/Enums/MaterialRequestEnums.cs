using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Domain.Enums
{
    public enum MaterialRequestStatus
    {
        Open,
        PendingApproval,
        Approved,
        Rejected,
        Fulfilled,
        Closed
    }

    public enum MaterialRequestPriority
    {
        Low,
        Normal,
        Urgent
    }

    public enum FulfillmentType
    {
        FromStock,
        Purchase,
        Transfer
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Application.MaterialRequests
{
    public class RejectionMaterialRequest
    {
        public int Id { get; set; }
        public string? RejectionReason { get; set; }
    }
}

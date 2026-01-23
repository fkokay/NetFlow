using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Application.MaterialRequests
{
    public class CreateMaterialRequest
    {
        public string RequestType { get; set; } = null!;
        public DateTime? RequiredDate { get; set; }
        public string Priority { get; set; } = "Normal";
        public string? RequestedDepartment { get; set; }
        public string? Description { get; set; }
        public string? SourceReference { get; set; }
    }
}

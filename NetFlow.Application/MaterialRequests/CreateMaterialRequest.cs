using NetFlow.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Text;

namespace NetFlow.Application.MaterialRequests
{
    public class CreateMaterialRequest
    {
        public MaterialRequestType RequestType { get; set; } = MaterialRequestType.Project;
        public DateTime RequiredDate { get; set; }
        public MaterialRequestPriority Priority { get; set; } = MaterialRequestPriority.Normal;
        public string? RequestedDepartment { get; set; }
        public string? Description { get; set; }
        public MaterialRequestSourceType SourceType { get; set; } = MaterialRequestSourceType.None;
    }
}

using NetFlow.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.ReadModel.MaterialRequestHistories
{
    public class MaterialRequestHistoryDto
    {
        public int Id { get; set; }
        public int MaterialRequestId { get; set; }
        public MaterialRequestHistoryAction Action { get; set; }
        public string? Notes { get; set; }
        public int ActionByUserId { get; set; }
        public DateTime ActionDate { get; set; } = DateTime.UtcNow;
    }
}

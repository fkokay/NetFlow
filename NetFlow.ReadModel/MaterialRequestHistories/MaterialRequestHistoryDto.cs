using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.ReadModel.MaterialRequestHistories
{
    public class MaterialRequestHistoryDto
    {
        public int Id { get; set; }
        public int MaterialRequestId { get; set; }
        public string Action { get; set; } = null!;// Created / Approved / Rejected / Fulfilled
        public string? Notes { get; set; }
        public int ActionByUserId { get; set; }
        public DateTime ActionDate { get; set; } = DateTime.UtcNow;
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Domain.Entities
{
    public class RequestEntity
    {
        public int Id { get; set; }
        public int FirmId { get; set; }
        public string RequestType { get; set; } = default!;
        public string Priority { get; set; } = default!;
        public string Status { get; set; } = default!;
        public string Subject { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int RelatedId { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? ClosedAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

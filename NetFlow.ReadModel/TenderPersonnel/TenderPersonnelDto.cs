using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.ReadModel.TenderPersonnel
{
    public class TenderPersonnelDto
    {
        public int Id { get; set; }
        public int TenderId { get; set; }
        public int PersonnelId { get; set; }
        public string PersonnelCode { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string TenderCode { get; set; } = string.Empty;
        public string TenderName { get; set; } = string.Empty;
        public string FirmName { get; set; } = string.Empty;
        public decimal? Salary { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

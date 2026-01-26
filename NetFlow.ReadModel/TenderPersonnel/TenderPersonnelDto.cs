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
        public string PersonnelCode { get; set; }
        public string FullName { get; set; }
        public string TenderCode { get; set; }
        public string TenderName { get; set; }
        public string FirmName { get; set; }
        public decimal? Salary { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

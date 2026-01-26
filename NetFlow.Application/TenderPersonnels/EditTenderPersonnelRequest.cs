using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Application.TenderPersonnels
{
    public class EditTenderPersonnelRequest
    {
        public int TenderId { get; set; }
        public List<int> PersonnelIds { get; set; } = new();
    }
}

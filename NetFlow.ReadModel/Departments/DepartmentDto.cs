using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.ReadModel.Departments
{
    public class DepartmentDto
    {
        public int Id { get; set; }
        public string DepartmentCode { get; set; } = null!;
        public string DepartmentName { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

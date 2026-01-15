using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NetFlow.Blazor.Shared.Models
{
    public class ModuleModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Modül Kodu zorunludur")]
        public string Code { get; set; } = string.Empty;
        [Required(ErrorMessage = "Modül Adı zorunludur")]
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NetFlow.Blazor.Shared.Models
{
    public class RoleModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Ad zorunludur")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Ad zorunludur")]
        public string Code { get; set; } = string.Empty;
    }
}

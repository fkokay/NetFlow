using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NetFlow.Domain.Entities
{
    [Table("ServiceFormDocument")]
    public class ServiceFormDocumentEntity
    {
        [Key]
        public int Id { get; set; }
        public int ServiceFormId { get; set; }
        public byte ImageType { get; set; }
        public string FileName { get; set; } = null!;
        public string FileExtension { get; set; } = null!;
        public int? FileSizeKB { get; set; }
        public string FilePath { get; set; } = null!;
        public string? ThumbnailPath { get; set; }
        public string? Description { get; set; }
        public DateTime? TakenAt { get; set; }
        public int? TakenBy { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual ServiceFormEntity ServiceForm { get; set; } = null!;
        public virtual PersonnelEntity? TakenByPersonnel { get; set; }
    }
}

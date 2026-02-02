using NetFlow.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NetFlow.ReadModel.ServiceFormDocuments
{
    public class ServiceFormDocumentDto
    {
        public int Id { get; set; }
        public int ServiceFormId { get; set; }
        [NotMapped]
        public string ServiceFormNo { get; set; } = null!;
        public ImageType ImageType { get; set; }
        public string FileName { get; set; } = null!;
        public string FileExtension { get; set; } = null!;
        public int? FileSizeKB { get; set; }
        public string FilePath { get; set; } = null!;
        public string? ThumbnailPath { get; set; }
        public string? Description { get; set; }
        public DateTime? TakenAt { get; set; }
        public int? TakenBy { get; set; }
        [NotMapped]
        public string? TakenByPersonnelCode { get; set; }
        [NotMapped]
        public string? TakenByPersonnelName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.ReadModel.TenderDocuments
{
    public sealed class TenderRequiredDocumentDto
    {
        public int Id { get; set; }
        public int TenderId { get; set; }
        public int DocumentId { get; set; }

        public string DocumentName { get; set; }

        public bool IsMandatory { get; set; }
        public bool Submitted { get; set; }
        public DateTime? SubmissionDate { get; set; }

        public int? TenderRequiredDocumentFileId { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public byte[] FileContent { get; set; }
    }
}

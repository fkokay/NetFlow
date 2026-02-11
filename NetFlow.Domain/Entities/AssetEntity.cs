using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Domain.Entities
{
    public class AssetEntity
    {
        public int Id { get; set; }

        public int? TenderId { get; set; }
        public int? TenderDeviceId { get; set; }

        public string AssetCode { get; set; }
        public string SerialNumber { get; set; }

        public string? AssetName { get; set; }
        public string? AssetType { get; set; }

        public string? Brand { get; set; }
        public string? Model { get; set; }

        public byte AssetStatus { get; set; }

        public DateTime InstallationDate { get; set; }
        public string? LocationCode { get; set; }
        public string? LocationName { get; set; }

        public DateTime? WarrantyExpiryDate { get; set; }
        public bool HasMaintenancePlan { get; set; }

        public string? NetsisStockCode { get; set; }
        public string? NetsisTransactionRef { get; set; }
        public bool IsFromNetsis { get; set; }

        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Domain.Enums
{
    public enum MaterialRequestType
    {
        Production = 1, // Üretim
        Maintenance = 2, // Bakım / Onarım
        Office = 3, // Ofis / Genel
        Project = 4, // Proje bazlı
        RnD = 5 // Ar-Ge
    }

    public enum MaterialRequestStatus
    {
        Draft = 0, // Taslak (henüz gönderilmedi)
        Open = 1, // Talep açıldı
        PendingApproval = 2, // Onay bekliyor
        Approved = 3, // Onaylandı
        Rejected = 4, // Reddedildi
        InProgress = 5, // Karşılanıyor (satın alma / stok / transfer)
        Fulfilled = 6, // Karşılandı
        Closed = 7, // Kapandı
        Cancelled = 8 // İptal edildi
    }

    public enum MaterialRequestPriority
    {
        Low = 1, // Düşük
        Normal = 2, // Normal (default)
        High = 3, // Yüksek
        Urgent = 4 // Acil
    }

    public enum MaterialRequestSourceType
    {
        None = 0,
        WorkOrder = 1, // İş Emri
        Project = 2, // Proje
        Tender = 3, // İhale
        Maintenance = 4, // Bakım Talebi
        SalesOrder = 5, // Satış Siparişi
        ProductionOrder = 6 // Üretim Emri
    }

    public enum MaterialRequestItemFulfillmentType
    {
        Undefined = 0, // Henüz belirlenmedi
        FromStock = 1, // Stoktan karşılandı
        Purchase = 2, // Satın alma yapıldı
        Transfer = 3, // Depolar arası transfer
        Production = 4, // Üretimden karşılandı
        Outsourcing = 5, // Dış tedarik / fason
        Return = 6 // İade / geri kazanım
    }
    public enum MaterialRequestItemStatus
    {
        Pending = 1, // Bekliyor
        Partial = 2, // Kısmi karşılandı
        Fulfilled = 3, // Tam karşılandı
        Cancelled = 4 // İptal edildi
    }


    public enum MaterialRequestHistoryAction
    {
        Created = 1, // Oluşturuldu
        Approved = 2, // Onaylandı
        Rejected = 3, // Reddedildi
        Fulfilled = 4 // Karşılandı / Tamamlandı
    }
}

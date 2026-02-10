using System.ComponentModel;

namespace NetFlow.Blazor.Shared.Models
{
    public enum MaterialRequestType
    {
        //[Description("Üretim")]
        //Production = 1, // Üretim
        //[Description("Bakım / Onarım")]
        //Maintenance = 2, // Bakım / Onarım
        //[Description("Ofis / Genel")]
        //Office = 3, // Ofis / Genel
        //[Description("Proje bazlı")]
        //Project = 4, // Proje bazlı
        //[Description("Ar-Ge")]
        //RnD = 5 // Ar-Ge
        [Description("İhale")]
        Tender = 1, 
    }

    public enum MaterialRequestStatus
    {
        [Description("Taslak")]
        Draft = 0, // Taslak (henüz gönderilmedi)
        [Description("Talep açıldı")]
        Open = 1, // Talep açıldı
        [Description("Onay bekliyor")]
        PendingApproval = 2, // Onay bekliyor
        [Description("Onaylandı")]
        Approved = 3, // Onaylandı
        [Description("Reddedildi")]
        Rejected = 4, // Reddedildi
        [Description("Karşılanıyor")]
        InProgress = 5, // Karşılanıyor (satın alma / stok / transfer)
        [Description("Karşılandı")]
        Fulfilled = 6, // Karşılandı
        [Description("Kapandı")]
        Closed = 7, // Kapandı
        [Description("İptal edildi")]
        Cancelled = 8 // İptal edildi
    }

    public enum MaterialRequestPriority
    {
        [Description("Düşük")]
        Low = 1, // Düşük
        [Description("Normal")]
        Normal = 2, // Normal (default)
        [Description("Yüksek")]
        High = 3, // Yüksek
        [Description("Acil")]
        Urgent = 4 // Acil
    }

    public enum MaterialRequestSourceType
    {
        [Description("Yok")]
        None = 0,
        [Description("İş Emri")]
        WorkOrder = 1, // İş Emri
        [Description("Proje")]
        Project = 2, // Proje
        [Description("İhale")]
        Tender = 3, // İhale
        [Description("Bakım Talebi")]
        Maintenance = 4, // Bakım Talebi
        [Description("Satış Siparişi")]
        SalesOrder = 5, // Satış Siparişi
        [Description("Üretim Emri")]
        ProductionOrder = 6 // Üretim Emri
    }

    public enum MaterialRequestItemFulfillmentType
    {
        [Description("Henüz belirlenmedi")]
        Undefined = 0, // Henüz belirlenmedi
        [Description("Stok")]
        FromStock = 1, // Stok
        [Description("Satın alma")]
        Purchase = 2, // Satın alma
    }

    public enum MaterialRequestItemStatus
    {
        [Description("Bekliyor")]
        Pending = 1, // Bekliyor
        [Description("Kısmi karşılandı")]
        Partial = 2, // Kısmi karşılandı
        [Description("Tam karşılandı")]
        Fulfilled = 3, // Tam karşılandı
        [Description("İptal edildi")]
        Cancelled = 4 // İptal edildi
    }


    public enum MaterialRequestHistoryAction
    {
        [Description("Oluşturuldu")]
        Created = 1, // Oluşturuldu
        [Description("Onaylandı")]
        Approved = 2, // Onaylandı
        [Description("Reddedildi")]
        Rejected = 3, // Reddedildi
        [Description("Karşılandı")]
        Fulfilled = 4 // Karşılandı
    }
}

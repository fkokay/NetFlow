using System.ComponentModel;

namespace NetFlow.Blazor.Shared.Models
{


    public enum ServiceType
    {
        [Description("Tanımsız")]
        Undefined = 0,

        [Description("Bakım")]
        Maintenance = 1,

        [Description("Onarım")]
        Repair = 2,

        [Description("Kurulum")]
        Installation = 3,

        [Description("Kontrol / İnceleme")]
        Inspection = 4,

        [Description("Teknik Destek")]
        TechnicalSupport = 5
    }

    public enum ServiceStatus
    {
        [Description("Taslak")]
        Draft = 0,

        [Description("Açık")]
        Open = 1,

        [Description("Personele Atandı")]
        Assigned = 2,

        [Description("İşlemde")]
        InProgress = 3,

        [Description("Parça Bekleniyor")]
        WaitingForParts = 4,

        [Description("Müşteri Bekleniyor")]
        WaitingForCustomer = 5,

        [Description("Tamamlandı")]
        Completed = 6,

        [Description("İptal Edildi")]
        Cancelled = 7,

        [Description("Kapatıldı")]
        Closed = 8
    }

    public enum ServiceDetailType
    {
        [Description("Tanımsız")]
        Undefined = 0,

        [Description("Malzeme / Yedek Parça")]
        Material = 1,

        [Description("Masraf (Yol, Konaklama vb.)")]
        Expense = 2,

        [Description("Servis Bedeli")]
        ServiceFee = 3,
    }

    public enum ServiceActionType
    {
        [Description("Tanımsız")]
        Undefined = 0,

        [Description("Servis Formu Oluşturuldu")]
        Created = 1,

        [Description("Güncellendi")]
        Updated = 2,

        [Description("Statü Değiştirildi")]
        StatusChanged = 3,

        [Description("Personel Atandı / Değiştirildi")]
        PersonnelAssigned = 4,

        [Description("Servise Başlandı")]
        ServiceStarted = 5,

        [Description("Servis Tamamlandı")]
        ServiceCompleted = 6,

        [Description("Servis Kapatıldı")]
        ServiceClosed = 7,

        [Description("Servis İptal Edildi")]
        Cancelled = 8,

        [Description("Not Eklendi")]
        NoteAdded = 9
    }

    public enum ImageType
    {
        [Description("Bilinmiyor")]
        Unknown = 0,

        [Description("Fotoğraf")]
        Photo = 1,

        [Description("Doküman")]
        Document = 2,

        [Description("İmza")]
        Signature = 3,

        [Description("Video")]
        Video = 4
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Domain.Enums
{
    public enum ServiceType
    {
        Undefined = 0,      // Güvenli default
        Maintenance = 1,    // Bakım
        Repair = 2,         // Onarım
        Installation = 3,   // Kurulum
        Inspection = 4,     // Kontrol / İnceleme
        TechnicalSupport = 5 // Teknik destek
    }
    public enum ServiceStatus
    {
        Draft = 0,          // Taslak (henüz açılmış ama işlem yok)
        Open = 1,           // Açık
        Assigned = 2,       // Personele atandı
        InProgress = 3,     // İşlemde
        WaitingForParts = 4,// Parça bekleniyor
        WaitingForCustomer = 5, // Müşteri bekleniyor
        Completed = 6,      // Tamamlandı
        Cancelled = 7,      // İptal edildi
        Closed = 8          // Kapatıldı (faturalandı vb.)
    }

    public enum ServiceDetailType
    {
        Undefined = 0,      // Güvenli default
        Labor = 1,          // İşçilik
        Material = 2,       // Malzeme / Yedek Parça
        Expense = 3,        // Masraf (yol, konaklama vb.)
        ServiceFee = 4,     // Servis bedeli (sabit ücret)
        Discount = 5,       // Satır bazlı indirim
        AdditionalCharge = 6 // Ek ücret
    }

    public enum ServiceActionType
    {
        Undefined = 0,          // Güvenli default
        Created = 1,            // Servis formu oluşturuldu
        Updated = 2,            // Genel güncelleme
        StatusChanged = 3,      // Statü değişti
        PersonnelAssigned = 4,  // Personel atandı/değişti
        ServiceStarted = 5,     // Servise başlandı
        ServiceCompleted = 6,   // Servis tamamlandı
        ServiceClosed = 7,      // Servis kapatıldı
        Cancelled = 8,          // Servis iptal edildi
        NoteAdded = 9           // Not eklendi
    }
    public enum ImageType
    {
        Unknown = 0,
        Photo = 1,
        Document = 2,
        Signature = 3,
        Video = 4
    }

}

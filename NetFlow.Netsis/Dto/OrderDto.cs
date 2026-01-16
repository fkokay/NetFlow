using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NetFlow.Netsis.Dto
{
    public class OrderDto
    {
        [Key, Column(Order = 0)]
        public short SUBE_KODU { get; set; }
        [Key, Column(Order = 1)]
        [StringLength(15)]
        public string FTIRSIP { get; set; } = null!;
        [Key, Column(Order = 2)]
        [StringLength(16)]
        public string FATIRS_NO { get; set; } = null!;
        [Key, Column(Order = 3)]
        [StringLength(16)]
        public string CARI_KODU { get; set; } = null!;
        public string CARI_ISIM { get; set; } = null!;
        public DateTime TARIH { get; set; }
        public byte? TIPI { get; set; }
        public double? BRUTTUTAR { get; set; }
        public double? SAT_ISKT { get; set; }
        public double? MFAZ_ISKT { get; set; }
        public double? GEN_ISK1T { get; set; }
        public double? GEN_ISK2T { get; set; }
        public double? GEN_ISK3T { get; set; }
        public double? GEN_ISK1O { get; set; }
        public double? GEN_ISK2O { get; set; }
        public double? GEN_ISK3O { get; set; }
        public double? KDV { get; set; }
        public double? FAT_ALTM1 { get; set; }
        public double? FAT_ALTM2 { get; set; }
        public double? FAT_ALTM3 { get; set; }
        [StringLength(20)]
        public string? ACIKLAMA { get; set; }
        [StringLength(1)]
        public string? KOD1 { get; set; }
        [StringLength(1)]
        public string? KOD2 { get; set; }
        public short? ODEMEGUNU { get; set; }
        public DateTime? ODEMETARIHI { get; set; }
        [StringLength(1)]
        public string? KDV_DAHILMI { get; set; }
        public short? FATKALEM_ADEDI { get; set; }
        public DateTime? SIPARIS_TEST { get; set; }
        public decimal? TOPLAM_MIK { get; set; }
        public short? TOPDEPO { get; set; }
        public short? TOPGIRDEPO { get; set; }
        public int SIRANO { get; set; }
        public double? KDV_DAHIL_BRUT_TOP { get; set; }
        public double? KDV_TENZIL { get; set; }
        public double? MALFAZLASIKDVSI { get; set; }
        public double GENELTOPLAM { get; set; }
        public double? YUVARLAMA { get; set; }
        public byte? DOVIZTIP { get; set; }
        public double? DOVIZTUT { get; set; }
        public DateTime? DOVBAZTAR { get; set; }
        [StringLength(8)]
        public string? PLA_KODU { get; set; }
        [StringLength(8)]
        public string? KS_KODU { get; set; }
        public double? BAG_TUTAR { get; set; }
        [StringLength(15)]
        public string? YAPKOD { get; set; }
        [StringLength(15)]
        public string? AMBAR_KBLNO { get; set; }
        [StringLength(25)]
        public string? PROJE_KODU { get; set; }
        [StringLength(8)]
        public string? KOSULKODU { get; set; }
        public DateTime? FIYATTARIHI { get; set; }
        public DateTime? KOSULTARIHI { get; set; }
        [StringLength(1)]
        public string ONAYTIPI { get; set; } = "A";
        public int ONAYNUM { get; set; }
        public short ISLETME_KODU { get; set; }
        [StringLength(16)]
        public string? GIB_FATIRS_NO { get; set; }
        public int? EBELGE { get; set; }
        public int? HALFAT { get; set; }
        [StringLength(100)]
        public string? EXTERNALAPPID { get; set; }
        [StringLength(100)]
        public string? EXTERNALREFID { get; set; }
        public string? KAYITYAPANKUL { get; set; }
        public DateTime? KAYITTARIHI { get; set; }
        public string? DUZELTMEYAPANKUL { get; set; }
        public DateTime? DUZELTMETARIHI { get; set; }
    }
}

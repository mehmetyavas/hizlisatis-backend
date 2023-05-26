using Core.Abstract;
using System;
using System.Collections.Generic;

namespace Entities.Concrete.Netsis;

public partial class Tblstsabit : IEntity
{
    public short SubeKodu { get; set; }

    public short IsletmeKodu { get; set; }

    public string StokKodu { get; set; } = null!;

    public string? UreticiKodu { get; set; }

    public string? StokAdi { get; set; }

    public string? GrupKodu { get; set; }

    public string? Kod1 { get; set; }

    public string? Kod2 { get; set; }

    public string? Kod3 { get; set; }

    public string? Kod4 { get; set; }

    public string? Kod5 { get; set; }

    public string? SaticiKodu { get; set; }

    public string? OlcuBr1 { get; set; }

    public string? OlcuBr2 { get; set; }

    public decimal? Pay1 { get; set; }

    public decimal? Payda1 { get; set; }

    public string? OlcuBr3 { get; set; }

    public decimal? Pay2 { get; set; }

    public decimal? Payda2 { get; set; }

    public string? FiatBirimi { get; set; }

    public decimal? AzamiStok { get; set; }

    public decimal? AsgariStok { get; set; }

    public decimal? TeminSuresi { get; set; }

    public decimal? KulMik { get; set; }

    public short? RiskSuresi { get; set; }

    public string? ZamanBirimi { get; set; }

    public decimal? SatisFiat1 { get; set; }

    public decimal? SatisFiat2 { get; set; }

    public decimal? SatisFiat3 { get; set; }

    public decimal? SatisFiat4 { get; set; }

    public byte? SatDovTip { get; set; }

    public decimal? DovAlisFiat { get; set; }

    public decimal? DovMalFiat { get; set; }

    public decimal? DovSatisFiat { get; set; }

    public int? MuhDetaykodu { get; set; }

    public decimal? BirimAgirlik { get; set; }

    public decimal? NakliyetTut { get; set; }

    public decimal? KdvOrani { get; set; }

    public byte? AlisDovTip { get; set; }

    public short? DepoKodu { get; set; }

    public byte? DovTur { get; set; }

    public byte? UretOlcuBr { get; set; }

    public string? Bilesenmi { get; set; }

    public string? Mamulmu { get; set; }

    public decimal? FormulToplami { get; set; }

    public string? UpdateKodu { get; set; }

    public decimal? MaxIskonto { get; set; }

    public decimal? EczaciKari { get; set; }

    public decimal? Miktar { get; set; }

    public decimal? MalFazlasi { get; set; }

    public decimal? KdvTenzilOran { get; set; }

    public string? Kilit { get; set; }

    public string? OncekiKod { get; set; }

    public string? SonrakiKod { get; set; }

    public string? Barkod1 { get; set; }

    public string? Barkod2 { get; set; }

    public string? Barkod3 { get; set; }

    public decimal? AlisKdvKodu { get; set; }

    public decimal? AlisFiat1 { get; set; }

    public decimal? AlisFiat2 { get; set; }

    public decimal? AlisFiat3 { get; set; }

    public decimal? AlisFiat4 { get; set; }

    public decimal? LotSize { get; set; }

    public decimal? MinSipMiktar { get; set; }

    public short? SabitSipAralik { get; set; }

    public string? SipPolitikasi { get; set; }

    public byte? OzellikKodu1 { get; set; }

    public byte? OzellikKodu2 { get; set; }

    public byte? OzellikKodu3 { get; set; }

    public byte? OzellikKodu4 { get; set; }

    public byte? OzellikKodu5 { get; set; }

    public byte? OpsiyonKodu1 { get; set; }

    public byte? OpsiyonKodu2 { get; set; }

    public byte? OpsiyonKodu3 { get; set; }

    public byte? OpsiyonKodu4 { get; set; }

    public byte? OpsiyonKodu5 { get; set; }

    public byte? BilesenOpKodu { get; set; }

    public decimal? SipVerMal { get; set; }

    public decimal? EldeBulMal { get; set; }

    public decimal? YilTahKulMik { get; set; }

    public decimal? EkonSipMiktar { get; set; }

    public string? EskiRecete { get; set; }

    public string? OtomatikUretim { get; set; }

    public string? Alfkod { get; set; }

    public string? Safkod { get; set; }

    public string? Kodturu { get; set; }

    public string? SYedek1 { get; set; }

    public string? SYedek2 { get; set; }

    public decimal? FYedek3 { get; set; }

    public decimal? FYedek4 { get; set; }

    public string? CYedek5 { get; set; }

    public string? CYedek6 { get; set; }

    public byte? BYedek7 { get; set; }

    public short? IYedek8 { get; set; }

    public int? LYedek9 { get; set; }

    public DateTime? DYedek10 { get; set; }

    public string? GirisSeri { get; set; }

    public string? CikisSeri { get; set; }

    public string? SeriBak { get; set; }

    public string? SeriMik { get; set; }

    public string? SeriGirOt { get; set; }

    public string? SeriCikOt { get; set; }

    public string? SeriBaslangic { get; set; }

    public string? Fiyatkodu { get; set; }

    public int? Fiyatsirasi { get; set; }

    public string? Planlanacak { get; set; }

    public decimal? LotSizecustomer { get; set; }

    public decimal? MinSipMiktarcustomer { get; set; }

    public string? Gumruktarifekodu { get; set; }

    public string? Abckodu { get; set; }

    public string? Performanskodu { get; set; }

    public string? Saticisipkilit { get; set; }

    public string? Musterisipkilit { get; set; }

    public string? Satinalmakilit { get; set; }

    public string? Satiskilit { get; set; }

    public decimal? En { get; set; }

    public decimal? Boy { get; set; }

    public decimal? Genislik { get; set; }

    public string? Siplimitvar { get; set; }

    public string? Sonstokkodu { get; set; }

    public string? Onaytipi { get; set; }

    public int? Onaynum { get; set; }

    public string? FiktifMam { get; set; }

    public string? Yapilandir { get; set; }

    public string? Sbomvarmi { get; set; }

    public string? Baglistokkod { get; set; }

    public string? Yapkod { get; set; }

    public string? Alistaltekkilit { get; set; }

    public string? Satistaltekkilit { get; set; }

    public string? SYedek3 { get; set; }

    public short? Stokmevzuat { get; set; }

    public string? Otvtevkifat { get; set; }

    public string? Seribarkod { get; set; }

    public string? AtikUrun { get; set; }
}

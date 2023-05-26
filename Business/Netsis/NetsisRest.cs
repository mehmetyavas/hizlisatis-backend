using NetOpenX.Rest.Client.Model;
using NetOpenX.Rest.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Business.Constants;
using Microsoft.Identity.Client;
using Core.Utilities.Results;
using Entities.DTOs.Netsis;
using NetOpenX.Rest.Client.BLL;
using NetOpenX.Rest.Client.Model.Enums;
using NetOpenX.Rest.Client.Model.NetOpenX;
using Newtonsoft.Json.Linq;

namespace Business.Netsis
{
    public class NetsisRest
    {

        ISettingDal _settingDal;

        public NetsisRest(ISettingDal settingDal)
        {
            _settingDal = settingDal;
        }

        public async Task<oAuth2> Token()
        {
            var ip = await _settingDal.GetAsync(x => x.Key == Settings.ip);
            var user = await _settingDal.GetAsync(x => x.Key == Settings.user);
            var password = await _settingDal.GetAsync(x => x.Key == Settings.password);
            var database = await _settingDal.GetAsync(x => x.Key == Settings.database);

            oAuth2 _oauth2 = new(ip.Value);
            await _oauth2.LoginAsync(new JLogin()
            {
                BranchCode = 0,
                NetsisUser = user.Value,
                NetsisPassword = password.Value,
                DbType = JNVTTipi.vtMSSQL,
                DbName = database.Value,
                DbPassword = "",
                DbUser = "TEMELSET"
            });
            return _oauth2;
        }


        public async Task<IResult> SendToNetsis(SendSaleToNetsisDTO sendSaleToNetsisDTOs)
        {
            ItemSlipsManager manager = new ItemSlipsManager(await Token());
            ItemSlips slips = new ItemSlips();
            JTFaturaTip jTip = new JTFaturaTip();

            var kdv = sendSaleToNetsisDTOs.IsVATIncluded;

            var tip = sendSaleToNetsisDTOs.DocumentType;

            var sabitCari = await _settingDal.GetAsync(x => x.Key == Settings.sabitCari);

            switch (tip)
            {
                case "1":
                    jTip = JTFaturaTip.ftSIrs;
                    break;

                case "2":
                    jTip = JTFaturaTip.ftSSip;
                    break;

                case "3":
                    jTip = JTFaturaTip.ftSFat;
                    break;

                default:
                    jTip = JTFaturaTip.ftSSip;
                    sendSaleToNetsisDTOs.Cari = sabitCari.Value;
                    break;
            }

            var seriDeger = await _settingDal.GetAsync(x => x.Key == Settings.seriDeger);

            slips.Seri = seriDeger.Value;
            slips.FatUst = await Header(jTip, kdv, sendSaleToNetsisDTOs);
            if (jTip == JTFaturaTip.ftSIrs)
            {
                //burası formdan gelecek
                slips.EIrsEkBilgi = new EWaybillInfo
                {
                    SOFOR1ADI = sendSaleToNetsisDTOs.TeslimAlan,
                    SOFOR1SOYADI = ".",
                    SOFOR1TCKN = "11111111111",
                    TASIYICIPOSTAKODU = "35860",
                    TASIYICIIL = "IZMIR",
                    TASIYICIILCE = "TORBALI",
                    TASIYICIULKE = "TR",
                    SEVKTAR = DateTime.Now,
                };
            }
            slips.Kalems = new List<ItemSlipLines>();
            foreach (var sale in sendSaleToNetsisDTOs.NetsisReqs)
            {
                slips.Kalems.Add(new ItemSlipLines
                {
                    S_Yedek2 = sale.Id.ToString(),
                    StokKodu = sale.StokKodu,
                    STra_GC = sale.GC,
                    STra_GCMIK = Convert.ToDouble(sale.GCMik),
                    STra_NF = Convert.ToDouble(sale.StraNF),
                    STra_BF = Convert.ToDouble(sale.StraBF),
                    STra_KDV = sale.StraKDV,
                    Ekalan = sale.EkAlan,
                    DEPO_KODU = sale.DepoKodu,
                    Ekalan1 = "denemeHSserver",
                    STra_ACIK = sendSaleToNetsisDTOs.paymentType == 4 ? sendSaleToNetsisDTOs.Cari : null,
                    STra_CARI_KOD = sendSaleToNetsisDTOs.paymentType == 4 ? sendSaleToNetsisDTOs.Cari : null,
                });
            }
            var netsisResult = await manager.PostInternalAsync(slips);
            if (netsisResult.IsSuccessful)
            {
                return new SuccessResult("Netsis Kaydı Basarılı " + netsisResult.Message);
            }
            else
            {
                return new ErrorResult("Başarısız" + netsisResult.ErrorDesc);
            }
        }

        private async Task<ItemSlipsHeader> Header(JTFaturaTip jTip, bool kdv, SendSaleToNetsisDTO sendSaleToNetsisDTOs)
        {
            var posKasa = await _settingDal.GetAsync(x => x.Key == Settings.PosKasa);

            var nakitKasa = await _settingDal.GetAsync(x => x.Key == Settings.NakitKasa);

            return sendSaleToNetsisDTOs.paymentType != 4
               ? new ItemSlipsHeader
               {
                   CariKod = sendSaleToNetsisDTOs.Cari,
                   Tarih = DateTime.Now,
                   TIPI = JTFaturaTipi.ft_Kapali,
                   KDV_DAHILMI = kdv,
                   Tip = jTip,
                   FiiliTarih = DateTime.Now,
                   SIPARIS_TEST = DateTime.Now,
                   KOD1 = "M",
                   PLA_KODU = "00",
                   Aciklama = "HS",
                   EIrsaliye = false,
                   KS_KODU = sendSaleToNetsisDTOs.paymentType == 3
                                ? posKasa.Value
                                : nakitKasa.Value
               }
               : new ItemSlipsHeader
               {
                   CariKod = sendSaleToNetsisDTOs.Cari,
                   Tarih = DateTime.Now,
                   TIPI = JTFaturaTipi.ft_Acik,
                   KDV_DAHILMI = kdv,
                   Tip = jTip,
                   FiiliTarih = DateTime.Now,
                   SIPARIS_TEST = DateTime.Now,
                   Aciklama = "HS",
                   KOD1 = "C",
                   EIrsaliye = true,
                   EKACK1 = "Teslim Alan : " + sendSaleToNetsisDTOs.TeslimAlan,
                   GIB_FATIRS_NO = $"{sendSaleToNetsisDTOs.fatNo.Substring(0, 3)}2023{sendSaleToNetsisDTOs.fatNo.Substring(6)}",
                   FATIRS_NO = sendSaleToNetsisDTOs.fatNo,
                   EKACK2 = "Teslim Eden : " + sendSaleToNetsisDTOs.TeslimEden,
               };

        }


    }
}

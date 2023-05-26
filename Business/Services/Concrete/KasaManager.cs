using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Business.Services.Abstract;
using Business.Services.Concrete.SaleHandler;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Migrations;
using Entities.Concrete;
using Entities.Concrete.Netsis;
using Entities.DTOs.Netsis;

namespace Business.Services.Concrete
{
    public class KasaManager : IKasaService
    {
        IKasaDal _kasaDal;
        IMapper _mapper;
        ISettingDal _settingDal;
        ISaleDal _saleDal;
        IEmployeeDal _employeeDal;
        IOutgoingDal _outgoingDal;
        ISaleOutgoingDal _saleOutgoingDal;
        public KasaManager(
            IKasaDal kasaDal,
            IMapper mapper,
            ISettingDal settingDal,
            ISaleDal saleDal,
            IEmployeeDal employeeDal,
            IOutgoingDal outgoingDal,
            ISaleOutgoingDal saleOutgoingDal)
        {
            _kasaDal = kasaDal;
            _mapper = mapper;
            _settingDal = settingDal;
            _saleDal = saleDal;
            _employeeDal = employeeDal;
            _outgoingDal = outgoingDal;
            _saleOutgoingDal = saleOutgoingDal;
        }

        public async Task<IResult> AddSaleToKasa(IEnumerable<Sale> sale)
        {

            var nakitResult = sale.Where(x =>
                                x.PaymentType == 1 ||
                                x.PaymentType == 2).ToList();

            if (nakitResult.Count > 0)
            {
                await KasaRecord(nakitResult, true);
            }


            var posResult = sale.Where(x => x.PaymentType == 3).ToList();

            if (posResult.Count > 0)
            {
                await KasaRecord(posResult, false);
            }


            foreach (var record in sale)
            {
                record.Transfer = true;
                record.UpdatedAt = DateTime.Now;

                await _saleDal.UpdateAsync(record);
            }

            return new SuccessResult("Kasaya İşlendi!");
        }
        public async Task<IResult> AddEmployeeOutgoingToKasa(IEnumerable<Sale> sale)
        {

            foreach (var sales in sale)
            {
                var getEmployee = await _employeeDal.GetAsync(x => x.Id == sales.EmployeeId);
                var sabitCari = await _settingDal.GetAsync(x => x.Key == Settings.sabitCari);
                var kasa = new TblKasaDTO
                {
                    KsmasKod = "02",
                    Tarih = DateTime.Now,
                    Fisno = sales.ShiftId + ".VARDIYA",
                    Io = "C",
                    Tip = "C",
                    Aciklama = getEmployee.Firstname + " - " + sales.StockName,
                    Tutar = sales.Price * sales.Amount,
                    CariMuh = "C",
                    Kod = sabitCari.Value,
                    PlasiyerKodu = "00",
                    SubeKodu = 0
                };
                var mappedKasa = _mapper.Map<Tblkasa>(kasa);

                await _kasaDal.AddAsync(mappedKasa);


               
                sales.Transfer = true;
                sales.UpdatedAt = DateTime.Now;

                await _saleDal.UpdateAsync(sales);

            }


            return new SuccessResult("Kasaya İşlendi!");
        }

        public async Task<IResult> AddOutgoingToKasa(List<SaleOutgoing> outgoings)
        {

            foreach (var outgoing in outgoings)
            {
                var getOutgoing = await _outgoingDal.GetAsync(x => x.Id == outgoing.OutgoingId);
                var sabit = await _settingDal.GetAsync(x => x.Key == Settings.sabitCari);
                var outGoingKasa = new TblKasaDTO
                {
                    KsmasKod = "02",
                    Tarih = DateTime.Now,
                    Fisno = outgoing.ShiftId + ".VARDIYA",
                    Io = "C",
                    Tip = "C",
                    Aciklama = getOutgoing.Name + "-" + outgoing.Description,
                    Tutar = outgoing.Price,
                    CariMuh = "C",
                    Kod = sabit.Value,
                    PlasiyerKodu = "00",
                    SubeKodu = 0
                };
                var mappedKasa = _mapper.Map<Tblkasa>(outGoingKasa);

                await _kasaDal.AddAsync(mappedKasa);
                outgoing.Transfer = true;
                outgoing.UpdatedAt = DateTime.Now;
                await _saleOutgoingDal.UpdateAsync(outgoing);
            }



            return new SuccessResult("Müşteri Gideri İşlendi");
        }


        public Tblkasa Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Tblkasa> GetAll()
        {
            throw new NotImplementedException();
        }


        public async Task<IResult> KasaRecord(List<Sale> sale, bool isNakit)
        {


            var totalCashResult = sale.Sum(x => x.Amount * x.Price);

            var nakitKasa = await _settingDal.GetAsync(x => x.Key == Settings.NakitKasa);
            var posKasa = await _settingDal.GetAsync(x => x.Key == Settings.PosKasa);
            var sabit = await _settingDal.GetAsync(x => x.Key == Settings.sabitCari);

            var kasa = new TblKasaDTO
            {
                KsmasKod = isNakit
                            ? nakitKasa.Value
                            : posKasa.Value,
                Tarih = DateTime.Now,
                Fisno = sale.Last().ShiftId + ".VARDIYA",
                Io = "G",
                Tip = "C",
                Aciklama = "HS",
                Tutar = totalCashResult,
                CariMuh = "C",
                Kod = sabit.Value,
                PlasiyerKodu = "00",
                SubeKodu = 0
            };

            var mappedKasa = _mapper.Map<Tblkasa>(kasa);

            await _kasaDal.AddAsync(mappedKasa);

            return new SuccessResult();
        }


    }
}
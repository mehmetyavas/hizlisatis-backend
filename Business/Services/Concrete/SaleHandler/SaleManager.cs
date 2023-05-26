using AutoMapper;
using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Constants;
using Business.Helpers;
using Business.Netsis;
using Business.Services.Abstract;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.Netsis;
using Entities.DTOs.Sale;

namespace Business.Services.Concrete.SaleHandler
{
    public class SaleManager : ISaleService
    {
        IMapper _mapper;
        ISaleDal _saleDal;
        IEmployeeDal _employeeDal;
        IUserDal _userDal;
        IShiftDal _shiftDal;
        IPaymentDal _paymentDal;
        ISettingDal _settingDal;
        ISaleOutgoingDal _saleOutgoingDal;

        IKasaService _kasaService;
        ITblFatuirsService _fatuirsService;
        private readonly ICustomerDal _customerDal;

        public SaleManager(
            ISaleDal saleDal,
            IEmployeeDal employeeDal,
            IUserDal userDal,
            IShiftDal shiftDal,
            IPaymentDal paymentDal,
            ISettingDal settingDal,
            ISaleOutgoingDal saleOutgoingDal,
            IKasaService kasaService,
            ITblFatuirsService fatuirsService,
            ICustomerDal customerDal,
            IMapper mapper)
        {
            _saleDal = saleDal;
            _employeeDal = employeeDal;
            _userDal = userDal;
            _shiftDal = shiftDal;
            _paymentDal = paymentDal;
            _kasaService = kasaService;
            _fatuirsService = fatuirsService;
            _settingDal = settingDal;
            _saleOutgoingDal = saleOutgoingDal;
            _customerDal = customerDal;
            _mapper = mapper;
        }

        //[SecuredOperation("admin,user")]
        public async Task<IResult> Add(SaleReqDTO sale)
        {
            string successMesage = null!;
            foreach (var sales in sale.Sales)
            {
                var result = BusinessRules.Run(
             await CheckIfPaymentIsCorrect(sale.paymentType),
             await CheckIfEmployeIsExist(sale.EmployeeId, sale.paymentType),
             await CheckIfUserIsExist(sale.UserId),
             await CheckIfShiftIsExist(sale.ShiftId)
              );

                if (result != null)
                    return result;
            }
            var mappedSaleResult = _mapper.Map<List<Sale>>(sale.Sales);

            foreach (var sales in mappedSaleResult)
            {
                sales.SlipId = await _saleDal.GetMaxSlipNo() + 1;
                sales.ShiftId = sale.ShiftId;
                sales.SlipId = sale.SlipId;
                sales.EmployeeId = sale.EmployeeId;
                sales.UserId = sale.UserId;
                sales.PaymentType = sale.paymentType;
                sales.CustomerCode = sale.CustomerCode;
                sales.CustomerDescription = sale.CustomerCode != null
                    ? _customerDal.Get(x => x.CariKod == sale.CustomerCode).CariIsim
                    : "";
            }

            var saleResult = await _saleDal.AddSlip(mappedSaleResult);
            if (saleResult.Success)
            {
                successMesage = saleResult.Message + " - ";


                //carileri aktarım koşuluna bakmaksızın aktarıyor
                if (sale.paymentType == 4)
                {
                    await Send(sale);
                }



                var transfer = await _settingDal.GetAsync(x => x.Key == Settings.vardiyaAktar);
                if (transfer.Value == "0")
                {


                    var sendToNetsis = sale.paymentType != 4 ? await Send(sale) : new SuccessResult(successMesage);

                    //Netsise aktarmak için gerekli verileri topluyor.

                    if (sendToNetsis.Success)
                    {

                        var maxSlip = await _saleDal.GetMaxSlipNo();

                        // PAymentType'ları 1,2,3 olan satışları aktarıyor
                        var cashRecord = await _saleDal
                        .GetListAsync(x =>
                                x.Transfer == false &&
                                x.PaymentType == 1 ||
                                x.PaymentType == 2 ||
                                x.PaymentType == 3 &&
                                x.CreatedAt.Value.ToString().Contains(DateTime.Today.ToString()) &&
                                x.ShiftId == sale.ShiftId &&
                                x.SlipId == maxSlip
                            );
                        if (cashRecord.Count() > 0)
                        {
                            var kasaResult = await _kasaService.AddSaleToKasa(cashRecord);
                            if (kasaResult.Success)
                                successMesage += kasaResult.Message + " - ";
                            else
                                throw new Exception("Kasa işlemede hata meydna geldi!");
                        }


                        // PAymentType'ları 5 olan satışları aktarıyor
                        var employeeRecord = await _saleDal.GetListAsync(x =>
                                x.PaymentType == 5 &&
                                x.Transfer == false &&
                                x.CreatedAt.Value.ToString().Contains(DateTime.Today.ToString()) &&
                                x.ShiftId == sale.ShiftId &&
                                x.SlipId == maxSlip
                                );

                        if (employeeRecord.Count() > 0)
                        {
                            var employeeResult = await _kasaService.AddEmployeeOutgoingToKasa(employeeRecord);

                            if (employeeResult.Success)
                                successMesage += employeeResult.Message + " - ";
                            else
                                throw new Exception();
                        }

                    }
                    else
                        throw new Exception(sendToNetsis.Message);
                }
            }
            else
                throw new Exception("satış yapmada hata meydana geldi");


            return new SuccessResult(successMesage);
        }

        [SecuredOperation("admin,sale.delete")]
        public async Task<IResult> DeleteSaleAsync(Sale sale)
        {
            await _saleDal.DeleteAsync(sale);
            return new SuccessResult(Messages.ProductDeleted);
        }

        public IDataResult<Sale> Get(int id)
        {
            var result = _saleDal.Get(x => x.Id == id);
            return new SuccessDataResult<Sale>(result);
        }

        public async Task<IDataResult<IEnumerable<SaleDTO>>> GetAll()
            => new SuccessDataResult<IEnumerable<SaleDTO>>(await _saleDal.GetSaleDetail());


        public async Task<IDataResult<IEnumerable<SaleDTO>>> GetSlipByDetail(int slipId)
            => new SuccessDataResult<IEnumerable<SaleDTO>>(await _saleDal.GetSaleDetail(x => x.slipId == slipId));

        public async Task<IDataResult<IEnumerable<SaleSummaryDTO>>> GetSaleSummariesAsync(DateTime? date, int userId)
        {
            return new SuccessDataResult<IEnumerable<SaleSummaryDTO>>(await _saleDal.GetSaleSummaries(date, userId));
        }


        public async Task<IDataResult<SaleSlipDTO>> GetSlipByMaxSlipIdAsync(SaleReqDTO req)
        {
            var maxSlip = await _saleDal.GetMaxSlipNo();

            var getMaxSlip = _saleDal.GetAll(x => x.SlipId == maxSlip);
            var user = _userDal.Get(x => x.Id == req.UserId);
            SaleSlipDTO slipDTO = new SaleSlipDTO
            {
                createdAt = DateTime.Now,
                slipId = maxSlip,
                payment = _paymentDal.Get(x => x.Id == req.paymentType).PaymentName,
                User = user.FirstName + " " + user.LastName!,
                Delivery = req.TeslimEden,
                Receiver = req.TeslimAlan
            };
            slipDTO.Sales = new List<SaleSlipReqDTO>();

            slipDTO.CustomerName = req.CustomerName;
            foreach (var sale in getMaxSlip)
            {
                slipDTO.Sales.Add(new SaleSlipReqDTO
                {
                    amount = sale.Amount,
                    price = sale.Price,
                    stockName = sale.StockName,
                    VATGroup = sale.VATGroup,
                });
            }



            var mappedResult = _mapper.Map<SaleSlipDTO>(slipDTO);

            return new SuccessDataResult<SaleSlipDTO>(mappedResult);
        }


        [SecuredOperation("admin,sale.update")]
        public async Task<IResult> UpdateSaleAsync(Sale sale)
        {
            var recordToUpdate = await _saleDal.GetAsync(x => x.Id == sale.Id);


            var result = BusinessRules.Run(
                await CheckIfPaymentIsCorrect(sale.PaymentType),
                await CheckIfEmployeIsExist(sale.EmployeeId, sale.PaymentType),
                await CheckIfUserIsExist(sale.UserId),
                await CheckIfShiftIsExist(sale.ShiftId)
                );

            if (result != null)
                throw new Exception(result.Message);

            var checkPaymentIsCorrect = await _paymentDal.GetAsync(x => x.Id == sale.PaymentType);


            recordToUpdate.Amount = sale.Amount;
            recordToUpdate.ShiftId = sale.ShiftId;
            recordToUpdate.SlipId = sale.SlipId;
            recordToUpdate.PaymentType = sale.PaymentType;
            recordToUpdate.StockCode = sale.StockCode;
            recordToUpdate.StockName = sale.StockName;
            recordToUpdate.EmployeeId = sale.EmployeeId;
            recordToUpdate.CustomerCode = sale.CustomerCode;
            recordToUpdate.CreatedAt = sale.CreatedAt;
            recordToUpdate.Price = sale.Price;
            recordToUpdate.Transfer = sale.Transfer;
            recordToUpdate.UpdatedAt = DateTime.Now;
            recordToUpdate.UserId = sale.UserId;
            recordToUpdate.VATGroup = sale.VATGroup;


            await _saleDal.UpdateAsync(recordToUpdate);

            return new SuccessResult("Record Updated");
        }

        public async Task<IDataResult<IEnumerable<Sale>>> getByShift(int shift)
        {

            var result = await _saleDal.GetListAsync(x =>
                     x.Transfer == false &&
                     x.ShiftId == shift
                     );
            return new SuccessDataResult<IEnumerable<Sale>>(result);
        }

        //Vardiya Kapanırken Kayıt Eder
        //[SecuredOperation("admin,user")]
        public async Task<IResult> AddByShift(List<SaleShiftDTO> sale)
        {
            string successMesage = null!;

            if (sale.Count > 0)
            {
                var result = await SendShift(sale);

                if (result.Success)
                {
                    // PAymentType'ları 1,2,3 olan satışları aktarıyor
                    var cashRecord = await _saleDal
                    .GetListAsync(x =>
                            x.Transfer == false &&
                            (x.PaymentType == 1 ||
                            x.PaymentType == 2 ||
                            x.PaymentType == 3)
                             &&
                            //x.CreatedAt.Value.ToString().Contains(DateTime.Today.ToString()) &&
                            x.ShiftId == sale.Last().ShiftId
                        );
                    if (cashRecord.Count() > 0)
                    {
                        var kasaResult = await _kasaService.AddSaleToKasa(cashRecord);
                        if (kasaResult.Success)
                            successMesage += kasaResult.Message + " - ";
                        else
                            throw new Exception("Kasa işlemede hata meydna geldi!");
                    }

                    // PAymentType'ları 5 olan satışları aktarıyor
                    var employeeRecord = await _saleDal.GetListAsync(x =>
                            x.PaymentType == 5 &&
                            x.Transfer == false &&
                            //x.CreatedAt.Value.ToString().Contains(DateTime.Today.ToString()) &&
                            x.ShiftId == sale.Last().ShiftId

                            );

                    if (employeeRecord.Count() > 0)
                    {
                        var employeeResult = await _kasaService.AddEmployeeOutgoingToKasa(employeeRecord);

                        if (employeeResult.Success)
                            successMesage += employeeResult.Message + " - ";
                        else
                            throw new Exception();

                    }

                }
                else
                    throw new Exception();
            }

            //payment 6 olanlar
            var outgoingRecord = _saleOutgoingDal.GetAll(x =>
                    x.Transfer == false &&
                    x.ShiftId == sale.Last().ShiftId
                    );
            if (outgoingRecord.Count > 0)
            {
                var outgoingResult = await _kasaService.AddOutgoingToKasa(outgoingRecord);

                if (outgoingResult.Success)
                    successMesage += outgoingResult.Message + " - ";
                else
                    throw new Exception();

            }






            return new SuccessResult(successMesage);

        }
        ////Satış anında aktarım metodu
        private async Task<IResult> Send(SaleReqDTO sales)
        {
            if (sales.Sales.Count < 1)
                throw new Exception();


            SendSaleToNetsisDTO sendSaleToNetsisDTOs = new SendSaleToNetsisDTO();

            sendSaleToNetsisDTOs.NetsisReqs = new List<NetsisReqDTO>();

            foreach (var sale in sales.Sales)
            {
                var mappedResult = _mapper.Map<Sale>(sale);
                sendSaleToNetsisDTOs.NetsisReqs.Add(await GetSendSales(mappedResult));

            }



            sendSaleToNetsisDTOs.IsVATIncluded = sales.IsVATIncluded;
            sendSaleToNetsisDTOs.ShiftId = sales.ShiftId;
            sendSaleToNetsisDTOs.DocumentType = sales.DocumentType;
            sendSaleToNetsisDTOs.Cari = sales.CustomerCode;
            sendSaleToNetsisDTOs.TeslimAlan = sales.TeslimAlan;
            sendSaleToNetsisDTOs.TeslimEden = sales.TeslimEden;
            sendSaleToNetsisDTOs.paymentType = sales.paymentType;
            sendSaleToNetsisDTOs.fatNo = sales.paymentType == 4
                  ? await GetFatNo(sales.CustomerCode != null ? sales.CustomerCode : "A", sales.DocumentType!)
                  : null;



            NetsisRest netsis = new NetsisRest(_settingDal);

            var result = await netsis.SendToNetsis(sendSaleToNetsisDTOs);
            if (result.Success)
            {
                if (sendSaleToNetsisDTOs.paymentType == 4)
                {
                    var customerSale = await _saleDal.GetListAsync(x =>
                                 x.Transfer == false &&
                                 x.PaymentType == 4);
                    foreach (var item in customerSale)
                    {
                        var sale = await _saleDal.GetAsync(x => x.Id == item.Id);

                        sale.Transfer = true;
                        await _saleDal.UpdateAsync(sale);
                    }


                }
            }

            return result.Success
                ? new SuccessResult("Netsis Kaydı Başarılı " + result.Message)
                : throw new Exception(result.Message);
        }

        //vardiya kapanırken aktarım metodu

        private async Task<IResult> SendShift(List<SaleShiftDTO> sales)
        {
            //var sales = _saleDal.GetAll(x =>
            //        x.Transfer == false &&
            //        x.CreatedAt.Value.ToString().Contains(DateTime.Today.ToString()) &&
            //        x.SlipId == _saleDal.GetMaxSlipNo()
            //        );

            if (sales.Count < 1)
                throw new Exception();


            //DTO nun içini dolduruyor
            SendSaleToNetsisDTO sendSaleToNetsisDTOs = new SendSaleToNetsisDTO();
            sendSaleToNetsisDTOs.NetsisReqs = new List<NetsisReqDTO>();
            foreach (var sale in sales)
            {
                var mappedReq = _mapper.Map<Sale>(sale);
                sendSaleToNetsisDTOs.NetsisReqs.Add(await GetSendSales(mappedReq));
                sendSaleToNetsisDTOs.ShiftId = mappedReq.ShiftId;
            }
            var sabitCari = await _settingDal.GetAsync(x => x.Key == Settings.sabitCari);
            sendSaleToNetsisDTOs.Cari = sabitCari.Value;


            NetsisRest netsis = new NetsisRest(_settingDal);

            var result = await netsis.SendToNetsis(sendSaleToNetsisDTOs);

            return result.Success
                ? new SuccessResult("Netsis Kaydı Başarılı " + result.Message)
                : throw new Exception(result.Message);

        }




        private async Task<NetsisReqDTO> GetSendSales(Sale sale)
        {
            NetsisReqDTO netsisReqDTOs = null!;

            var depoKod = await _settingDal.GetAsync(x => x.Key == Settings.depoKodu);

            netsisReqDTOs = new NetsisReqDTO
            {
                DepoKodu = int.Parse(depoKod.Value),
                EkAlan = sale.EmployeeId == null ? "" : _employeeDal.Get(x => x.Id == sale.EmployeeId).Firstname,
                GC = "C",
                GCMik = sale.Amount,
                StraNF = sale.Price,
                StraBF = sale.Price,
                StraKDV = double.Parse(sale.VATGroup),
                StokKodu = sale.StockCode,
                Id = sale.Id
            };

            return netsisReqDTOs;
        }


        private async Task<string> GetFatNo(string? cari, string document)
        {


            string ftIrsIp = null!;
            string seriBaslangic = "";
            var irsNo = await _settingDal.GetAsync(x => x.Key == Settings.IrsNo);
            var sipNo = await _settingDal.GetAsync(x => x.Key == Settings.SipNo);
            var fatNo = await _settingDal.GetAsync(x => x.Key == Settings.FatNo);
            switch (document)
            {
                case "1":
                    if (cari == null)
                    {
                        seriBaslangic = "B";
                        ftIrsIp = irsNo.Value;
                        break;
                    }
                    seriBaslangic = "AIR";
                    ftIrsIp = irsNo.Value;
                    break;
                case "2":
                    seriBaslangic = "B";
                    ftIrsIp = sipNo.Value;
                    break;
                case "3":
                    var identifier = await _fatuirsService.getIdentifier(cari);
                    if (identifier != null)
                    {
                        seriBaslangic = "AKB";
                        ftIrsIp = fatNo.Value;
                        break;
                    }
                    seriBaslangic = "ATA";
                    ftIrsIp = fatNo.Value;
                    break;
                default:
                    seriBaslangic = "H";
                    break;
            }
            var getFatNo = await _fatuirsService.GetMaxFatByFatNo(
                            ftIrsIp,
                            seriBaslangic);

            return GenerateSerialNo.serialNo(getFatNo);
        }

        #region Rules

        private async Task<IResult> CheckIfPaymentIsCorrect(int payment)
        {
            return await _paymentDal.GetAsync(x => x.Id == payment) == null
                ? throw new Exception("Böyle Bir Satış Tipi Yok")
                : new SuccessResult();

        }
        private async Task<IResult> CheckIfShiftIsExist(int shiftId)
        {
            return await _shiftDal.GetAsync(x => x.Id == shiftId) == null
                ? throw new Exception("Böyle bir Vardiya Yok")
                : new SuccessResult();
        }
        private async Task<IResult> CheckIfEmployeIsExist(int? employee, int payment)
        {
            if (payment == 5)
            {
                return await _employeeDal.GetAsync(x => x.Id == employee) == null
                    ? throw new Exception("Böyle bir Personel Yok")
                    : new SuccessResult();
            }
            else
            {
                return await _employeeDal.GetAsync(x => x.Id == employee) != null
                    ? throw new Exception("Personel Satışı Sadece Personel Satış tipinde Olmalı")
                    : new SuccessResult();
            }
        }
        private async Task<IResult> CheckIfUserIsExist(int userId)
        {
            return await _userDal.GetAsync(x => x.Id == userId) == null
                ? throw new Exception("Böyle bir Kullanıcı Yok")
                : new SuccessResult();
        }




        #endregion

    }
}

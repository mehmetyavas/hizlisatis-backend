using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Business.Netsis;
using Business.Services.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Entities.DTOs.Netsis;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concrete
{
    public class SaleOutgoingManager : ISaleOutgoingService
    {
        ISaleOutgoingDal _saleOutgoingDal;
        ISettingDal _settingDal;
        IKasaService _kasaService;
        IOutgoingService _outgoingService;
        IMapper _mapper;
        public SaleOutgoingManager(
            ISaleOutgoingDal saleOutgoingDal,
            ISettingDal settingDal,
            IKasaService kasaService,
            IOutgoingService outgoingService,
            IMapper mapper)
        {
            _saleOutgoingDal = saleOutgoingDal;
            _settingDal = settingDal;
            _kasaService = kasaService;
            _outgoingService = outgoingService;
            _mapper = mapper;
        }

        public async Task<IResult> Add(SaleOutgoingDto saleOutgoing)
        {

            var mappedResult = _mapper.Map<SaleOutgoing>(saleOutgoing);
            mappedResult.Transfer = false;

            await _saleOutgoingDal.AddAsync(mappedResult);

            var successMessage = "";
            var vardiyaAktar = await _settingDal.GetAsync(x => x.Key == Settings.vardiyaAktar);


            if (vardiyaAktar.Value == "0")
            {

                var outgoingRecord = await _saleOutgoingDal.GetListAsync(x =>
                        x.Transfer == false &&
                        x.CreatedAt.Value.ToString().Contains(DateTime.Today.ToString()) &&
                        x.ShiftId == saleOutgoing.ShiftId
                        );
                if (outgoingRecord.ToList().Count > 0)
                {
                    var outgoingResult = await _kasaService.AddOutgoingToKasa(outgoingRecord.ToList());
                    if (outgoingResult.Success)
                        successMessage += outgoingResult.Message + " - ";
                    else
                        throw new Exception("Kasa işlemede hata meydna geldi!");
                }

            }

            return new SuccessResult("Gider Kaydedildi " + successMessage);
        }

        public async Task<IResult> Delete(SaleOutgoing saleOutgoing)
        {
            var recordToDelete = await _saleOutgoingDal.GetAsync(x => x.Id == saleOutgoing.Id);
            await _saleOutgoingDal.DeleteAsync(recordToDelete);

            return new SuccessResult($"{recordToDelete.Description} - Silindi");
        }

        public async Task<SaleOutgoing> Get(int id)
        {
            return await _saleOutgoingDal.GetAsync(x => x.Id == id);
        }

        public async Task<List<SaleOutgoing>> GetListAsync()
        {
            var result = await _saleOutgoingDal.GetListAsync();
            return result.ToList();
        }

        public async Task<IResult> Update(SaleOutgoing saleOutgoing)
        {
            var recordToUpdate = await _saleOutgoingDal.GetAsync(x => x.Id == saleOutgoing.Id);

            recordToUpdate.Transfer = saleOutgoing.Transfer;
            recordToUpdate.Price = saleOutgoing.Price;
            recordToUpdate.CreatedAt = saleOutgoing.CreatedAt;
            recordToUpdate.Description = saleOutgoing.Description;
            recordToUpdate.ShiftId = saleOutgoing.ShiftId;
            recordToUpdate.OutgoingId = saleOutgoing.OutgoingId;

            await _saleOutgoingDal.UpdateAsync(recordToUpdate);


            return new SuccessResult($"{recordToUpdate.Description} - Güncellendi");
        }
    }
}

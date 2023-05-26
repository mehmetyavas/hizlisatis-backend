using Business.BusinessAspect.Autofac;
using Business.Services.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concrete
{
    public class ShiftManager : IShiftService
    {

        IShiftDal _shiftDal;

        public ShiftManager(IShiftDal shiftDal)
        {
            _shiftDal = shiftDal;
        }
        [SecuredOperation("admin")]
        public IResult Add(Shift shift)
        {
            _shiftDal.Add(shift);
            return new SuccessResult("Ekleme Başarılı");
        }
        [SecuredOperation("admin")]
        public IResult Delete(Shift shift)
        {
            _shiftDal.Delete(shift);
            return new SuccessResult("Silme Başarılı");
        }

        public List<Shift> GetAll()
        {
            return _shiftDal.GetAll();
        }

        public Shift GetById(int id)
        {
            return _shiftDal.Get(x => x.Id == id);
        }
        [SecuredOperation("admin")]
        public IResult Update(Shift shift)
        {
            var recordToUpdate = _shiftDal.Get(x => x.Id == shift.Id);

            recordToUpdate.Name = shift.Name;
            _shiftDal.Update(recordToUpdate);
            return new SuccessResult("Güncelleme Başarılı");
        }
    }
}

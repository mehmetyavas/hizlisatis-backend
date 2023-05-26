using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.BusinessAspect.Autofac;
using Business.Services.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Services.Concrete
{
    public class OutgoingManager : IOutgoingService
    {

        IOutgoingDal _outgoingDal;

        public OutgoingManager(IOutgoingDal outgoingDal)
        {
            _outgoingDal = outgoingDal;
        }
        [SecuredOperation("admin")]
        public async Task<IDataResult<Outgoing>> Add(Outgoing outgoing)
        {
            var result = await _outgoingDal.AddAsync(outgoing);
            return new SuccessDataResult<Outgoing>(result);
        }
        [SecuredOperation("admin")]
        public async Task<IResult> Delete(Outgoing outgoing)
        {
            var recordToDelete = await _outgoingDal.GetAsync(x => x.Id == outgoing.Id);

            await _outgoingDal.DeleteAsync(recordToDelete);
            return new SuccessResult($"{recordToDelete.Name} - Silindi!");
        }

        public async Task<Outgoing> Get(int id)
        {
            var result = await _outgoingDal.GetAsync(x => x.Id == id);
            return result;
        }

        public async Task<List<Outgoing>> GetAllAsync()
        {
            var result = await _outgoingDal.GetListAsync();
            return result.ToList();
        }
        [SecuredOperation("admin")]
        public async Task<IResult> UpdateAsync(Outgoing outgoing)
        {
            var recordToUpdate = await _outgoingDal.GetAsync(x => x.Id == outgoing.Id);

            recordToUpdate.Price = outgoing.Price;
            recordToUpdate.Name = outgoing.Name;
            await _outgoingDal.UpdateAsync(recordToUpdate);

            return new SuccessResult($"{recordToUpdate.Name} - Güncellendi!");
        }
    }
}

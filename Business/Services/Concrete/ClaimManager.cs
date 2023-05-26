using Business.BusinessAspect.Autofac;
using Business.Services.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concrete
{
    public class ClaimManager : IUserOperationClaimService
    {
        IUserOperationClaimDal _userClaimDal;
        IOperationClaimDal _operationClaimDal;

        IUserDal _userDal;
        public ClaimManager(IUserOperationClaimDal userClaimDal, IOperationClaimDal operationClaimDal, IUserDal userDal)
        {
            _userClaimDal = userClaimDal;
            _operationClaimDal = operationClaimDal;
            _userDal = userDal;
        }
        [SecuredOperation("admin")]
        public IResult AddClaim(UserOperationClaim claim)
        {

            IResult result = BusinessRules.Run(
                CheckIfClaimIsCorrect(claim.OperationClaimId),
                CheckIfUserIsCorrect(claim.UserId)
                );


            if (result != null)
            {
                return result;
            }

            _userClaimDal.Add(claim);

            return new SuccessResult("Başarılı");

        }
        [SecuredOperation("admin")]
        public IResult DeleteClaim(UserOperationClaim claim)
        {

            var result = BusinessRules.Run(CheckIfRequestIsCorrect(claim.Id));

            if (result != null)
            {
                return result;
            }
            _userClaimDal.Delete(claim);

            return new SuccessResult("Başarılı");
        }

        public async Task<IEnumerable<UserOperationClaim>> GetAllUserClaim()
        {

            return await _userClaimDal.GetListAsync();
        }

        public UserOperationClaim GetClaim(int id)
        {
            return _userClaimDal.Get(x => x.Id == id);
        }

        public List<UserOperationClaim> GetClaimByUserId(int id)
        {
            return _userClaimDal.GetAll(x => x.UserId == id);
        }

        [SecuredOperation("admin")]
        public IResult UpdateClaim(UserOperationClaim claim)
        {
            IResult result = BusinessRules.Run(
                CheckIfRequestIsCorrect(claim.Id),
                CheckIfClaimIsCorrect(claim.OperationClaimId),
                CheckIfUserIsCorrect(claim.UserId)
                );

            var recordToUpdate = _userClaimDal.Get(x => x.Id == claim.Id);
            recordToUpdate.OperationClaimId = claim.OperationClaimId;
            recordToUpdate.UserId = claim.UserId;

            if (result != null)
            {
                return result;
            }

            _userClaimDal.Update(recordToUpdate);
            return new SuccessResult("Güncelleme İşlemi Başarılı");
        }


        private IResult CheckIfUserIsCorrect(int UserId)
        {
            return _userDal.Get(x => x.Id == UserId) == null
                ? throw new Exception("Böyle Bir User Yok!")
                : new SuccessResult();
        }
        private IResult CheckIfClaimIsCorrect(int claimId)
        {

            return _operationClaimDal.Get(x => x.Id == claimId) == null
                ? throw new Exception("Böyle Bir Rol Yok!")
                : new SuccessResult();
        }

        private IResult CheckIfRequestIsCorrect(int Id)
        {
            var result = _userClaimDal.Get(x => x.Id == Id);
            if (result == null)
            {
                throw new Exception("Böyle bir kayıt yok");
            }
            return new SuccessResult();
        }
    }
}

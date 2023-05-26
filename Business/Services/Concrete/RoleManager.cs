using Business.BusinessAspect.Autofac;
using Business.Services.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concrete
{
    public class RoleManager : IRoleService
    {

        IOperationClaimDal _claimDal;

        public RoleManager(IOperationClaimDal claimDal)
        {
            _claimDal = claimDal;
        }
        [SecuredOperation("admin")]

        public IResult AddRole(OperationClaim role)
        {
            var result = BusinessRules.Run(CheckIfRoleIsExist(role.Name));

            if (result != null)
                return result;

            _claimDal.Add(role);

            return new SuccessResult("Rol Ekleme Başarılı!");
        }
        [SecuredOperation("admin")]

        public IResult DeleteRole(OperationClaim role)
        {
            _claimDal.Delete(role);

            return new SuccessResult("Rol Silme Başarılı!");
        }

        public List<OperationClaim> GetAllRole()
        {
            return _claimDal.GetAll();
        }

        public OperationClaim GetRole(int id)
        {
            return _claimDal.Get(x => x.Id == id);
        }
        [SecuredOperation("admin")]
        public IResult UpdateRole(OperationClaim role)
        {
            var result = BusinessRules.Run(CheckIfRoleIsExist(role.Name));

            if (result != null)
                return result;

            var recordToUpdate = _claimDal.Get(x => x.Id == role.Id);
            recordToUpdate.Name = role.Name;

            _claimDal.Update(recordToUpdate);

            return new SuccessResult("Rol Düzenleme Başarılı!");
        }


        private IResult CheckIfRoleIsExist(string role) 
        {
            return _claimDal.Get(x => x.Name == role)!=null
                ? throw new Exception("Böyle Bir Rol Zaten Var")
                : new SuccessResult();
        }
    }
}

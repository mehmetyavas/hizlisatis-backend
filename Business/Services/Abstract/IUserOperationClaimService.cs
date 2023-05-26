using Core.Entities.Concrete;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract
{
    public interface IUserOperationClaimService
    {
        Task<IEnumerable<UserOperationClaim>> GetAllUserClaim();
        List<UserOperationClaim> GetClaimByUserId(int id);
        UserOperationClaim GetClaim(int id);

        IResult AddClaim(UserOperationClaim claim);
        IResult UpdateClaim(UserOperationClaim claim);
        IResult DeleteClaim(UserOperationClaim claim);
    }
}

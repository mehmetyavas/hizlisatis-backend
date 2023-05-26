using Core.Entities.Concrete;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract
{
    public interface IRoleService
    {
        List<OperationClaim> GetAllRole();
        OperationClaim GetRole(int id);

        IResult AddRole(OperationClaim role);
        IResult UpdateRole(OperationClaim role);
        IResult DeleteRole(OperationClaim role);
    }
}

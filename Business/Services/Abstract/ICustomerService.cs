using Core.Utilities.Results;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract
{
    public interface ICustomerService
    {
        Task<List<TblcasabitDTO>> GetByQuery(string search);
        Task<TblcasabitDTO> Get(string CustomerCode);
    }
}

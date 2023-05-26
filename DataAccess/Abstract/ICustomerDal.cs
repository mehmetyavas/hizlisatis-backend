using Core.DataAccess;
using Entities.Concrete.Netsis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ICustomerDal:IEntityRepository<Tblcasabit>
    {
        public List<Tblcasabit> GetTop100(int take);
        public Task<List<Tblcasabit>> GetCustomerByTurkishChar(string search);
    }
}

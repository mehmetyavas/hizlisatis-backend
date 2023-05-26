using Core.DataAccess;
using Entities.Concrete.Netsis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{

    public interface IFatuirsDal : IEntityRepository<Tblfatuir>
    {


        Task<string> GetMaxFat(string ftIrsip, string fatNo);
        public Task<string> Identifier(string cari);
    }
}

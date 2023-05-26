using AutoMapper;
using Core.DataAccess.EntityFramework;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete;
using Entities.Concrete.Netsis;
using Entities.DTOs.Netsis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfKasaDal : EfEntityRepositoryBase<Tblkasa, Trn23Context>, IKasaDal
    {
       
        public EfKasaDal(Trn23Context context) : base(context)
        {
          
        }
    }
}

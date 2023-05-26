using AutoMapper;
using Entities.Concrete.Netsis;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.AutoMapper
{
    public class StockProfile:Profile
    {

        public StockProfile()
        {
            CreateMap<Tblstsabit,TblstsabitDTO>();
        }
    }
}

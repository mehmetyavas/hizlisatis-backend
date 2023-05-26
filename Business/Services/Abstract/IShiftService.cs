using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract
{
    public interface IShiftService
    {
        List<Shift> GetAll();
        Shift GetById(int id);

        IResult Add(Shift shift);
        IResult Update(Shift shift);
        IResult Delete(Shift shift);
    }
}

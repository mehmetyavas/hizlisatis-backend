using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract
{
    public interface ISettingService
    {
        List<Setting> GetAll();
        Setting GetById(int id);
        Setting GetByKey(string key);
        IResult Update(Dictionary<string, string> settings);

    }
}

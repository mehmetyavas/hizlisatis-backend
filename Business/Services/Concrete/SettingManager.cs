using Business.Services.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Services.Concrete
{
    public class SettingManager : ISettingService
    {
        ISettingDal _settingDal;

        public SettingManager(ISettingDal settingDal)
        {
            _settingDal = settingDal;
        }

        
        public List<Setting> GetAll()
        {
            return _settingDal.GetAll();
        }

        public Setting GetById(int id)
        {
            return _settingDal.Get(x=>x.Id== id);
        }

        public Setting GetByKey(string key)
        {
            return _settingDal.Get(x => x.Key == key);
        }

        public IResult Update(Dictionary<string, string> settings)
        {
            foreach (var setting in settings)
            {
                var recordToUpdate = _settingDal.Get(x => x.Key == setting.Key);

                recordToUpdate.Value = setting.Value;

                _settingDal.Update(recordToUpdate);
            }

           

            return new SuccessResult("Value Güncellendi");
        }
    }
}

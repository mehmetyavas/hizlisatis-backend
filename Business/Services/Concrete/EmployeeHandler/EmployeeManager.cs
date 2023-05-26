using Business.BusinessAspect.Autofac;
using Business.Services.Abstract;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concrete.EmployeeHandler
{
    public class EmployeeManager : IEmployeeService
    {
        IEmployeeDal _employeeDal;

        public EmployeeManager(IEmployeeDal employeeDal)
        {
            _employeeDal = employeeDal;
        }

        [SecuredOperation("admin")]
        public async Task<IDataResult<Employee>> AddEmployeeAsync(Employee employee)
        {
            var rulesResult = BusinessRules.Run(checkIfPhoneNumberExist(employee.PhoneNumber));

            if (rulesResult != null)
                throw new Exception(rulesResult.Message);

            var result = await _employeeDal.AddAsync(employee);
            return new SuccessDataResult<Employee>(result);
        }


        [SecuredOperation("admin")]
        public async Task<IResult> DeleteAsync(Employee employee)
        {
            await _employeeDal.DeleteAsync(employee);
            return new SuccessResult("başarılı");
        }

        public Employee GetEmployeeById(int id)
        {
            return _employeeDal.Get(x => x.Id == id);
        }

        public Employee GetEmployeeByName(string name)
        {
            return _employeeDal.Get(x => x.Firstname == name);
        }

        public async Task<List<Employee>> GetEmployeeListAsync()
        {
            var result = await _employeeDal.GetListAsync();
            return result.ToList();
        }
        [SecuredOperation("admin")]
        public async Task<IDataResult<Employee>> UpdateEmployeeAsync(Employee employee)
        {

            var recordToUpdate = await _employeeDal.GetAsync(x => x.Id == employee.Id);
            recordToUpdate.Firstname = employee.Firstname;
            recordToUpdate.PhoneNumber = employee.PhoneNumber;
            recordToUpdate.Lastname = employee.Lastname;
            var result = await _employeeDal.UpdateAsync(recordToUpdate);
            return new SuccessDataResult<Employee>(result);
        }

        private IResult checkIfPhoneNumberExist(string phone)
        {
            return _employeeDal.Get(x => x.PhoneNumber == phone) != null
                ? throw new Exception("Bu Numara Kayıtlı!")
                : new SuccessResult();
        }

    }
}

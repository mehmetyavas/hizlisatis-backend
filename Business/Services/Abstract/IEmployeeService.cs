using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetEmployeeListAsync();
        Employee GetEmployeeById(int id);
        Employee GetEmployeeByName(string name);
        Task<IDataResult<Employee>> AddEmployeeAsync(Employee employee);
        Task<IDataResult<Employee>> UpdateEmployeeAsync(Employee employee);
        Task<IResult> DeleteAsync(Employee employee);
    }
}

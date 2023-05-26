using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concrete.EmployeeHandler
{
    public class EmployeeValidator:AbstractValidator<Employee>
    {
        public EmployeeValidator()
        {
            RuleFor(x=>x.Firstname.Length).GreaterThan(0).NotEmpty().NotNull();
            RuleFor(x=>x.Lastname.Length).GreaterThan(0).NotEmpty().NotNull();
            RuleFor(x=>x.PhoneNumber.Length).GreaterThan(9).NotEmpty().NotNull();
        }
    }
}

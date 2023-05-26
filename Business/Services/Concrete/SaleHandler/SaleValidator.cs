using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concrete.SaleHandler
{
    public class SaleValidator:AbstractValidator<Sale>
    {
        public SaleValidator()
        {
            RuleFor(x=>x.Amount).GreaterThan(0).NotEmpty().NotNull();
            RuleFor(x=>x.Price).GreaterThan(0).NotEmpty().NotNull();
        }
    }
}

using Core.Entities.Concrete;
using Entities.DTOs.UserDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concrete.AuthHandler
{
    public class AuthValidator:AbstractValidator<UserForRegisterDto>
    {
        public AuthValidator()
        {
            RuleFor(x=>x.FirstName.Length).GreaterThan(2).NotEmpty();
            RuleFor(x=>x.LastName.Length).GreaterThan(0).NotEmpty();
            RuleFor(x=>x.Username.Length).GreaterThan(5).NotEmpty();
            RuleFor(x=>x.Password.Length).GreaterThan(5).NotEmpty();
        }
    }
}

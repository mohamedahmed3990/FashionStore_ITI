using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FashionStore.BLL.DTOs;
using FluentValidation;
using Microsoft.SqlServer.Server;

namespace FashionStore.BLL.Validators
{
    public class LoginDtoValidations : AbstractValidator<LoginDto>
    {
        public LoginDtoValidations()
        {
            RuleFor(L=>L.Email).NotEmpty().WithMessage("Email cant be empty")
                                .EmailAddress().WithMessage("Invalid email format.");


            RuleFor(L=>L.Password).NotEmpty().WithMessage("Password cant be empty");
            RuleFor(L => L.RememberMe).NotNull().WithMessage("Remember me must be true or false");
        }
    }
}

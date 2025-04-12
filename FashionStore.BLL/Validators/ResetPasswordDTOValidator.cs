using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FashionStore.BLL.DTOs;
using FluentValidation;

namespace FashionStore.BLL.Validators
{
    public class ResetPasswordDTOValidator : AbstractValidator<ResetPasswordDTO>
    {
        public ResetPasswordDTOValidator()
        {
            RuleFor(RP => RP.Email)
                    .NotEmpty().WithMessage("Email is required.")
                    .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(RP => RP.NewPassword)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters.")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches("[0-9]").WithMessage("Password must contain at least one number.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.")
                .Must(p => p.Distinct().Count() >= 2).WithMessage("Password must contain at least 2 different characters.");
        }
    }
}

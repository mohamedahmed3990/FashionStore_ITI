using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FashionStore.BLL.DTOs;
using FluentValidation;

namespace FashionStore.BLL.Validators
{
    public class PersonalInformationDTOValidators : AbstractValidator<PersonalInformationDTO>
    {
        public PersonalInformationDTOValidators()
        {
            RuleFor(R => R.FirstName)
                .NotEmpty()
                .WithMessage("First Name Cant be Empty")
                .MinimumLength(3)
                .WithMessage("Name cant be less than 3 characters");

            RuleFor(R => R.LastName)
                .NotEmpty()
                .WithMessage("Last Name Cant be Empty")
                .MinimumLength(3)
                .WithMessage("Name cant be less than 3 characters");

            RuleFor(R => R.UserName)
                .NotEmpty().WithMessage("Username is required.")
                .MinimumLength(3).WithMessage("Username must be at least 3 characters.")
                .MaximumLength(20).WithMessage("Username must be at most 20 characters.");

            RuleFor(R => R.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^(010|011|012|015)[0-9]{8}$")
                .WithMessage("Phone number must start with 010, 011, 012, or 015 and be 11 digits long.");
        }
    }
}

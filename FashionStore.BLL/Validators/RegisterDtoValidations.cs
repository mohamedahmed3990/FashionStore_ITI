using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FashionStore.BLL.DTOs;
using FashionStore.DAL.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace FashionStore.BLL.Validators
{
    public class RegisterDtoValidations : AbstractValidator<RegisterDto>
    {
        private readonly UserManager<AppUser> _userManager;

        public RegisterDtoValidations(UserManager<AppUser> userManager)
        {
            _userManager = userManager;

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

            RuleFor(R => R.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.")
                .MustAsync(async(email,cancellationToken) =>
                {
                    var user = await _userManager.FindByEmailAsync(email);
                    return user == null;
                })
                .WithMessage("Email is already registered.");

            RuleFor(R => R.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters.")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches("[0-9]").WithMessage("Password must contain at least one number.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.")
                .Must(p => p.Distinct().Count() >= 2).WithMessage("Password must contain at least 2 different characters.");


            RuleFor(R=>R.ConfirmPassword)
                .NotEmpty().WithMessage("Confirm Password is required.")
                .Equal(x => x.Password).WithMessage("Passwords do not match.");


            RuleFor(R => R.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^(010|011|012|015)[0-9]{8}$")
                .WithMessage("Phone number must start with 010, 011, 012, or 015 and be 11 digits long.");
                
            
        }
    }
}

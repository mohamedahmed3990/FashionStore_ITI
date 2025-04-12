using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FashionStore.BLL.DTOs;
using FluentValidation;

namespace FashionStore.BLL.Validators
{
    public class UserAddressDtoValidators : AbstractValidator<UserAddressDto>
    {
        public UserAddressDtoValidators()
        {
            RuleFor(A => A.City).NotEmpty().WithMessage("City is Required");
            RuleFor(A => A.Country).NotEmpty().WithMessage("Country is Required");
            RuleFor(A => A.AddressDetails).NotEmpty().WithMessage("Please Enter you address details");
        }
    }
}

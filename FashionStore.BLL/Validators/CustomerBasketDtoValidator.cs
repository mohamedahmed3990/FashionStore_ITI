using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FashionStore.BLL.DTOs;
using FluentValidation;

namespace FashionStore.BLL.Validators
{
    public class CustomerBasketDtoValidator : AbstractValidator<CustomerBasketDto>
    {
        public CustomerBasketDtoValidator()
        {
            RuleFor(b => b.Id)
            .NotEmpty().WithMessage("Basket Id is required.");

            RuleFor(b => b.Items)
                .NotEmpty().WithMessage("Basket must contain at least one item.");

            RuleForEach(b => b.Items)
                .SetValidator(new BasketItemDtoValidator());
        }
    }
}

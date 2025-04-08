using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FashionStore.BLL.DTOs;
using FluentValidation;

namespace FashionStore.BLL.Validators
{
    public class BasketItemDtoValidator : AbstractValidator<BasketItemDto>
    {
        public BasketItemDtoValidator()
        {
            RuleFor(b => b.Id)
                .NotEmpty().WithMessage("ID Is Required");

            RuleFor(p => p.ProductName)
                .NotEmpty().WithMessage("Product name is required");

            RuleFor(p => p.PictureUrl)
                .NotEmpty().WithMessage("Picture URL is required");

            RuleFor(p => p.Color)
                .NotEmpty().WithMessage("Color is required");

            RuleFor(p => p.Size)
                .NotEmpty().WithMessage("Size is required");

            RuleFor(p => p.Category)
               .NotEmpty().WithMessage("Category is required");

            RuleFor(p => p.Price)
                .NotEmpty().WithMessage("Price is required")
                .GreaterThanOrEqualTo(1).WithMessage("Price must be grater than 0");

            RuleFor(p => p.Quantity)
                .NotEmpty().WithMessage("Quantity is required")
                .GreaterThanOrEqualTo(1).WithMessage("Quantity must be grater than 0");

        }

    }
}   

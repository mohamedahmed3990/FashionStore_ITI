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
                .NotEmpty().WithMessage("ID Is Required")
                .WithErrorCode("INVALID ID");

            RuleFor(p => p.ProductName)
                .NotEmpty().WithMessage("Product name is required.")
                .WithErrorCode("PRODUCT_NAME_REQUIRED");

            RuleFor(p => p.PictureUrl)
                .NotEmpty().WithMessage("Picture URL is required.")
                .WithErrorCode("PICTURE_REQUIRED");

            RuleFor(p => p.Color)
                .NotEmpty().WithMessage("Color is required.")
                .WithErrorCode("COLOR_REQUIRED");

            RuleFor(p => p.Size)
                .NotEmpty().WithMessage("Size is required.")
                .WithErrorCode("SIZE_REQUIRED");

            RuleFor(p => p.Price)
                .NotEmpty().WithMessage("Price is required.")
                .GreaterThan(0).WithMessage("Price must be greater than 0.")
                .LessThanOrEqualTo(int.MaxValue).WithMessage($"Price must be in range.")
                .WithErrorCode("PRICE_INVALID");

            RuleFor(p => p.Quantity)
                .NotEmpty().WithMessage("Quantity is required.")
                .GreaterThan(0).WithMessage("Quantity must be greater than 0.")
                .LessThanOrEqualTo(int.MaxValue).WithMessage($"Quantity must be in range")
                .WithErrorCode("QUANTITY_MISMATCH");
        }

    }
}   

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using FashionStore.BLL.DTOs;
using FashionStore.BLL.Services.BasketService;
using FashionStore.BLL.Services.PaymentService;
using FashionStore.DAL.Entities.OrderAggregate;
using FashionStore.DAL.Interfaces;
using FashionStore.DAL.UnitOfWork;

namespace FashionStore.BLL.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentService _paymentService;

        public OrderService(IBasketRepository basketRepo, IUnitOfWork unitOfWork, IPaymentService paymentService)
        {
            _basketRepo = basketRepo;
            _unitOfWork = unitOfWork;
            _paymentService = paymentService;
        }

        public async Task<OrderToReturnDto?> CreateOrderAsync(string buyerEmail, string basketId, decimal shippingfee, Address shippingAddress)
        {
            var basket = await _basketRepo.GetBasketAsync(basketId);

            var orderItems = new List<OrderItem>();

            if (basket?.Items?.Count > 0) 
            {
                foreach (var item in basket.Items)
                {
                    var productVariant = await _unitOfWork.ProductVariantRepo.GetProductVariantAsync(item.Id);
                    if (productVariant is null) return null;

                    var product = await _unitOfWork.ProductRepo.GetProductAsync(productVariant.ProductId);
                    if (product is null) return null;

                    var productItemOrderd = new ProductItemOrdered(product.Id, product.ProductName, product.ProductPicture, productVariant.Color.Name, productVariant.Size.Name);

                    var orderItem = new OrderItem(productItemOrderd, productVariant.Price, item.Quantity);
                
                    orderItems.Add(orderItem);
                }
            }

            var subTotal = orderItems.Sum(orderItem => orderItem.Price *  orderItem.Quantity);

            var orderRepo = _unitOfWork.OrderRepo;
            var existOrder = await orderRepo.GetOrderByPaymentIntentId(basket.PaymentIntentId);

            if(existOrder is not null)
            {
                orderRepo.Delete(existOrder);
                await _paymentService.CreateOrUpdatePaymentIntent(basketId);
            }


            var order = new Order(buyerEmail, shippingAddress, shippingfee, orderItems, subTotal, basket.PaymentIntentId);
            
             _unitOfWork.OrderRepo.Add(order);

            var resutl = await _unitOfWork.SaveChangesAsync();

            if (resutl <= 0) { return null; }
          
            return MapOrderToDto(order); 

        }

        public async Task<OrderToReturnDto?> GetOrderByIdForUserAsync(int orderId, string buyerEmail)
        {
            var order = await _unitOfWork.OrderRepo.GetOrderByUserAsync(orderId, buyerEmail);
            if (order == null) { return null; }

            return MapOrderToDto(order);
        }

        public async Task<List<OrderToReturnDto>> GetOrdersForUserAsync(string buyerEmail)
        {
            var orders = await _unitOfWork.OrderRepo.GetOrdersByUserAsync(buyerEmail);

            return orders.Select(o => MapOrderToDto(o)).ToList();
        }

        public OrderToReturnDto MapOrderToDto(Order order)
        {
            return new OrderToReturnDto
            {
                Id = order.Id,
                BuyerEmail = order.BuyerEmail,
                OrderDate = order.OrderDate,
                Status = order.Status.ToString(),
                ShippingAddress = order.ShippingAddress,
                ShippingFee = order.ShippingFee,
                SubTotal = order.SubTotal,
                Total = order.GetTotal(),
                Items = order.Items.Select(item => new OrderItemDto
                {
                    ProductId = item.Product.ProductId,
                    ProductName = item.Product.ProductName,
                    ProductPicture = item.Product.ProductPicture,
                    ProductColor = item.Product.ProductColor,
                    ProductSize = item.Product.ProductSize,
                    Price = item.Price,
                    Quantity = item.Quantity
                }).ToList(),
                PaymentIntentId = order.PaymentIntentId
            };
        }
    }
}

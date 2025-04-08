using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FashionStore.BLL.Services.BasketService;
using FashionStore.DAL.Entities.OrderAggregate;
using FashionStore.DAL.Interfaces;
using FashionStore.DAL.UnitOfWork;

namespace FashionStore.BLL.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IBasketRepository basketRepo, IUnitOfWork unitOfWork)
        {
            _basketRepo = basketRepo;
            _unitOfWork = unitOfWork;
        }
        public async Task<Order?> CreateOrderAsync(string buyerEmail, string basketId, decimal shippingfee, Address shippingAddress)
        {
            var basket = await _basketRepo.GetBasketAsync(basketId);

            var orderItems = new List<OrderItem>();

            if (basket?.Items?.Count > 0) 
            {
                foreach (var item in basket.Items)
                {
                    var productVariant = await _unitOfWork.ProductVariantRepo.GetProductVariantAsync(item.Id);

                    var product = await _unitOfWork.ProductRepo.GetProductAsync(productVariant.ProductId);

                    var productItemOrderd = new ProductItemOrdered(product.ProductName, product.ProductPicture, productVariant.Color.Name, productVariant.Size.Name);

                    var orderItem = new OrderItem(productItemOrderd, productVariant.Price, item.Quantity);
                
                    orderItems.Add(orderItem);
                }
            }

            var subTotal = orderItems.Sum(orderItem => orderItem.Price *  orderItem.Quantity);  


            var order = new Order(buyerEmail, shippingAddress, shippingfee, orderItems, subTotal);

             _unitOfWork.OrderRepo.Add(order);

            var resutl = await _unitOfWork.SaveChangesAsync();

            if (resutl <= 0) { return null; }

            return order;

        }

        public Task<Order> GetOrderByIdForUserAsync(string orderId, string buyerEmail)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var orders = await _unitOfWork.OrderRepo.GetOrdersByUserAsync(buyerEmail);

            return orders.ToList();
        }
    }
}

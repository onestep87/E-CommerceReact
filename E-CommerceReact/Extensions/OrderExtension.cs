using E_CommerceReact.DTO;
using E_CommerceReact.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceReact.Extensions
{
    public static class OrderExtensions
    {
        public static IQueryable<OrderDTO> ProjectOrderToOrderDto(this IQueryable<Order> query)
        {
            return query
                .Select(order => new OrderDTO
                {
                    Id = order.Id,
                    BuyerId = order.BuyerId,
                    OrderDate = order.OrderDate,
                    ShippingAddress = order.ShippingAddress,
                    DeliveryFee = order.DeliveryFee,
                    Subtotal = order.Subtotal,
                    OrderStatus = order.OrderStatus.ToString(),
                    Total = order.GetTotal(),
                    OrderItems = order.OrderItems.Select(item => new OrderItemDTO
                    {
                        ProductId = item.ItemOrdered.ProductId,
                        Name = item.ItemOrdered.Name,
                        PictureUrl = item.ItemOrdered.PictureUrl,
                        Price = item.Price,
                        Quantity = item.Quantity
                    })
                    .ToList()
                }).AsNoTracking();
        }
    }
}

using E_CommerceReact.Entities.OrderAggregate;
using System.ComponentModel.DataAnnotations;

namespace E_CommerceReact.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string BuyerId { get; set; }
        public DateTime OrderDate { get; set; }
        public ShippingAddress ShippingAddress { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; }
        public long Subtotal { get; set; }
        public long DeliveryFee { get; set; }
        public string OrderStatus { get; set; }

        public long Total { get; set; }
    }
}

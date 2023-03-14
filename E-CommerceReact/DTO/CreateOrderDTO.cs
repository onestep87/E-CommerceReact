using E_CommerceReact.Entities.OrderAggregate;

namespace E_CommerceReact.DTO
{
    public class CreateOrderDTO
    {
        public bool SaveAddress { get; set; }
        public ShippingAddress ShippingAddress { get; set; }
        
    }
}

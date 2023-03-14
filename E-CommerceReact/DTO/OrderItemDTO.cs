namespace E_CommerceReact.DTO
{
    public class OrderItemDTO
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public long Price { get; set; }
        public int Quantity { get; set; }
        public string PictureUrl { get; set; }
    }
}

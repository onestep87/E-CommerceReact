namespace E_CommerceReact.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public long Price { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }
        public int QuantityInStock { get; set; }
        public string PublicId { get; set; }
        //public int Rating { get; set; }
        //public int NumReviews { get; set; }
    }
}

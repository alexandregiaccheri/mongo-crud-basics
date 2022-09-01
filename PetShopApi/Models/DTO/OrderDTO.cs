namespace PetShopApi.Models.DTO
{
    public class OrderDTO
    {
        public List<Cart> OrderItems { get; set; } = null!;

        public string OrderStatus { get; set; } = null!;

        public string PaymentStatus { get; set; } = null!;

        public Address ShippingAddress { get; set; } = null!;
    }
}

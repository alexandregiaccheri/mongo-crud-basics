using System.ComponentModel.DataAnnotations;

namespace PetShopApi.Models.DTO
{
    public class CreateOrderDTO
    {
        [Required]
        public List<Cart> OrderItems { get; set; } = null!;

        [Required]
        public string OrderStatus { get; set; } = null!;

        [Required]
        public string PaymentStatus { get; set; } = null!;

        [Required]
        public Address ShippingAddress { get; set; } = null!;

        [Required]
        public string UserId { get; set; } = null!;
    }
}

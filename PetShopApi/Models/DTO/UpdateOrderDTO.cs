namespace PetShopApi.Models.DTO
{
    public class UpdateOrderDTO
    {
        public string? OrderStatus { get; set; } = null!;

        public string? PaymentStatus { get; set; } = null!;
    }
}

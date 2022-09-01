namespace PetShopApi.Models.DTO
{
    public class ProductDTO
    {
        public string CategoryId { get; set; } = null!;

        public string ImgUrl { get; set; } = null!;

        public string ProductDescription { get; set; } = null!;

        public string ProductManufacturer { get; set; } = null!;

        public int ProductPrice { get; set; }

        public string ProductTitle { get; set; } = null!;

        public int Stock { get; set; }
    }
}

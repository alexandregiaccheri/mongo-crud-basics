using System.ComponentModel.DataAnnotations;

namespace PetShopApi.Models.DTO
{
    public class CreateProductDTO
    {
        [Required]
        public string CategoryId { get; set; } = null!;

        [Required]
        public string ImgUrl { get; set; } = null!;

        [Required]
        public string ProductDescription { get; set; } = null!;

        [Required]
        public string ProductManufacturer { get; set; } = null!;

        [Required]
        public int ProductPrice { get; set; }

        [Required]
        public string ProductTitle { get; set; } = null!;

        [Required]
        public int Stock { get; set; }
    }
}

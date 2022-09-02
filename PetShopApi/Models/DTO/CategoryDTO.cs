using System.ComponentModel.DataAnnotations;

namespace PetShopApi.Models.DTO
{
    public class CategoryDTO
    {
        [Required]
        public string CategoryName { get; set; } = null!;
    }
}

using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.DTOs.CategoryDTOs
{
    public class CategoryCreateDTO
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Categor Name Cant Exceed 100 Chaharcter")]
        public string Name { get; set; }
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "IsActive status is required.")]
        public bool IsActive { get; set; }
    }
}

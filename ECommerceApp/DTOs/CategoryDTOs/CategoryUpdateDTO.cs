using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.DTOs.CategoryDTOs
{
    public class CategoryUpdateDTO
    {
        [Required(ErrorMessage = "Id is required.")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Category Name can't exceed 100 characters.")]
        public string Name { get; set; }
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "IsActive status is required.")]
        public bool IsActive { get; set; }
    }
}

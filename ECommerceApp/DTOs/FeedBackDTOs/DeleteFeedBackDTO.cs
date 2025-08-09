using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.DTOs.FeedBackDTOs
{
    public class DeleteFeedBackDTO
    {
        [Required(ErrorMessage = "Feedback ID is required.")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Customer ID is required.")]
        public int CustomerId { get; set; }
    }
}

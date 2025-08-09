using ECommerceApp.Data;
using ECommerceApp.DTOs;
using ECommerceApp.DTOs.FeedBackDTOs;
using ECommerceApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Services
{
    public class FeedBackService
    {
        #region Fields
        private readonly ApplicationDbContext _context;
        #endregion

        #region Constructor
        public FeedBackService(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods
        public async Task<ApiResponse<FeedBackResponseDTO>> CreateFeedback(CreateFeedBackDTO createFeedBackDTO)
        {
            try
            {
                if (createFeedBackDTO == null)
                {
                    return new ApiResponse<FeedBackResponseDTO>(404, "Invalid feedback data.");
                }
                var customer = await _context.Customers.AsNoTracking().FirstOrDefaultAsync(customer => customer.Id == createFeedBackDTO.CustomerId);
                if (customer == null)
                {
                    return new ApiResponse<FeedBackResponseDTO>(404, "Customer not found.");
                }
                var product = await _context.Products.AsNoTracking().FirstOrDefaultAsync(product => product.Id == createFeedBackDTO.ProductId);
                if (product == null)
                {
                    return new ApiResponse<FeedBackResponseDTO>(404, "Product not found.");
                }

                var orderItem = await _context.OrderItems
                    .AsNoTracking()
                    .Include(oi => oi.Order)
                    .FirstOrDefaultAsync(oi => oi.ProductId == createFeedBackDTO.ProductId && oi.Order.CustomerId == createFeedBackDTO.CustomerId && oi.Order.OrderStatus == OrderStatus.Delivered);
                if (orderItem == null)
                {
                    return new ApiResponse<FeedBackResponseDTO>(404, "Feedback can only be given for products that have been delivered.");
                }
                var existingFeedback = await _context.Feedbacks
                    .AsNoTracking()
                    .FirstOrDefaultAsync(f => f.CustomerId == createFeedBackDTO.CustomerId && f.ProductId == createFeedBackDTO.ProductId);
                if (existingFeedback != null)
                {
                    return new ApiResponse<FeedBackResponseDTO>(400, "Feedback for this product by this customer already exists.");
                }

                var feedback = new Feedback
                {
                    CustomerId = createFeedBackDTO.CustomerId,
                    ProductId = createFeedBackDTO.ProductId,
                    Rating = createFeedBackDTO.Rating,
                    Comment = createFeedBackDTO.Comment,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                _context.Feedbacks.Add(feedback);
                await _context.SaveChangesAsync();
                var responseDto = new FeedBackResponseDTO
                {
                    Id = feedback.Id,
                    CustomerId = feedback.CustomerId,
                    ProductId = feedback.ProductId,
                    Rating = feedback.Rating,
                    Comment = feedback.Comment,
                    CreatedAt = feedback.CreatedAt,
                    UpdatedAt = feedback.UpdatedAt
                };
                return new ApiResponse<FeedBackResponseDTO>(200, responseDto);
            }
            catch (Exception ex)
            {
                return new ApiResponse<FeedBackResponseDTO>(500, $"An error Occured while Submitting FeedBack ,Error: {ex.Message}");
            }
        }
        public async Task<ApiResponse<ProductFeedbackResponseDTO>> GetFeedbackForProductAsync(int productId)
        {
            try
            {
                // Verify product exists
                var product = await _context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == productId);
                if (product == null)
                {
                    return new ApiResponse<ProductFeedbackResponseDTO>(404, "Product not found.");
                }
                // Retrieve feedbacks for the specified product, including customer details, with no tracking for performance
                var feedbacks = await _context.Feedbacks
                .Where(f => f.ProductId == productId)
                .Include(f => f.Customer)
                .AsNoTracking()
                .ToListAsync();
                double averageRating = 0;
                List<CustomerFeedback> customerFeedbacks = new List<CustomerFeedback>();
                if (feedbacks.Any())
                {
                    averageRating = feedbacks.Average(f => f.Rating);
                    customerFeedbacks = feedbacks.Select(f => new CustomerFeedback
                    {
                        Id = f.Id,
                        CustomerId = f.CustomerId,
                        CustomerName = $"{f.Customer.FirstName} {f.Customer.LastName}",
                        Rating = f.Rating,
                        Comment = f.Comment,
                        CreatedAt = f.CreatedAt,
                        UpdatedAt = f.UpdatedAt
                    }).ToList();
                }
                var productFeedbackResponse = new ProductFeedbackResponseDTO
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    AverageRating = Math.Round(averageRating, 2),
                    Feedbacks = customerFeedbacks
                };
                return new ApiResponse<ProductFeedbackResponseDTO>(200, productFeedbackResponse);
            }
            catch (Exception ex)
            {
                // Log the exception (implementation depends on your logging setup)
                return new ApiResponse<ProductFeedbackResponseDTO>(500, $"An unexpected error occurred while processing your request, Error: {ex.Message}");
            }
        }
        public async Task<ApiResponse<List<FeedBackResponseDTO>>> GetAllFeedBacks()
        {
            try
            {
                var feedbacks = await _context.Feedbacks
                .Include(f => f.Customer)
                .Include(f => f.Product)
                .AsNoTracking()
                .ToListAsync();
                var feedbackResponseList = feedbacks.Select(f => new FeedBackResponseDTO
                {
                    Id = f.Id,
                    CustomerId = f.CustomerId,
                    CustomerName = $"{f.Customer.FirstName} {f.Customer.LastName}",
                    ProductId = f.ProductId,
                    ProductName = f.Product.Name,
                    Rating = f.Rating,
                    Comment = f.Comment,
                    CreatedAt = f.CreatedAt,
                    UpdatedAt = f.UpdatedAt
                }).ToList();
                return new ApiResponse<List<FeedBackResponseDTO>>(200, feedbackResponseList);
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<FeedBackResponseDTO>>(500, $"An Error Occured While Retriving FeedBacks ,Error : {ex.Message}");
            }
        }

        // Updates an existing feedback entry.
        public async Task<ApiResponse<FeedBackResponseDTO>> UpdateFeedbackAsync(UpdateFeedBackDTO feedbackUpdateDTO)
        {
            try
            {
                // Retrieve the feedback along with its customer and product information
                var feedback = await _context.Feedbacks
                .Include(f => f.Customer)
                .Include(f => f.Product)
                .FirstOrDefaultAsync(f => f.Id == feedbackUpdateDTO.Id
                && f.CustomerId == feedbackUpdateDTO.CustomerId);
                if (feedback == null)
                {
                    return new ApiResponse<FeedBackResponseDTO>(404, "Either Feedback or Customer not found.");
                }
                // Update the feedback details
                feedback.Rating = feedbackUpdateDTO.Rating;
                feedback.Comment = feedbackUpdateDTO.Comment;
                feedback.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
                var feedbackResponse = new FeedBackResponseDTO
                {
                    Id = feedback.Id,
                    CustomerId = feedback.CustomerId,
                    CustomerName = $"{feedback.Customer.FirstName} {feedback.Customer.LastName}",
                    ProductId = feedback.ProductId,
                    ProductName = feedback.Product.Name,
                    Rating = feedback.Rating,
                    Comment = feedback.Comment,
                    CreatedAt = feedback.CreatedAt,
                    UpdatedAt = feedback.UpdatedAt
                };
                return new ApiResponse<FeedBackResponseDTO>(200, feedbackResponse);
            }
            catch (Exception ex)
            {
                // Log the exception (implementation depends on your logging setup)
                return new ApiResponse<FeedBackResponseDTO>(500, $"An unexpected error occurred while processing your request, Error: {ex.Message}");
            }
        }
        // Deletes a feedback entry.
        public async Task<ApiResponse<ConfirmationResponseDTO>> DeleteFeedbackAsync(DeleteFeedBackDTO feedbackDeleteDTO)
        {
            try
            {
                var feedback = await _context.Feedbacks.FindAsync(feedbackDeleteDTO.Id);
                if (feedback == null)
                {
                    return new ApiResponse<ConfirmationResponseDTO>(404, "Feedback not found.");
                }
                // Ensure that only the owner can delete the feedback
                if (feedback.CustomerId != feedbackDeleteDTO.CustomerId)
                {
                    return new ApiResponse<ConfirmationResponseDTO>(401, "You are not authorized to delete this feedback.");
                }
                _context.Feedbacks.Remove(feedback);
                await _context.SaveChangesAsync();
                var confirmation = new ConfirmationResponseDTO
                {
                    Message = $"Feedback with Id {feedbackDeleteDTO.Id} deleted successfully."
                };
                return new ApiResponse<ConfirmationResponseDTO>(200, confirmation);
            }
            catch (Exception ex)
            {
                // Log the exception (implementation depends on your logging setup)
                return new ApiResponse<ConfirmationResponseDTO>(500, $"An unexpected error occurred while processing your request, Error: {ex.Message}");
            }
        }
    }

    #endregion
}

using ECommerceApp.DTOs;
using ECommerceApp.DTOs.FeedBackDTOs;
using ECommerceApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedBackController : ControllerBase
    {
        #region Fields
        private readonly FeedBackService _feedBackService;
        #endregion

        #region Constructor
        public FeedBackController(FeedBackService feedBackService)
        {
            _feedBackService = feedBackService;
        }
        #endregion

        #region methods
        // Submits feedback for a product.
        [HttpPost("SubmitFeedback")]
        public async Task<ActionResult<ApiResponse<FeedBackResponseDTO>>> SubmitFeedback([FromBody] CreateFeedBackDTO feedbackCreateDTO)
        {
            var response = await _feedBackService.CreateFeedback(feedbackCreateDTO);
            if (response.StatusCode != 200)
            {
                return StatusCode(response.StatusCode, response);
            }
            return Ok(response);
        }
        // Retrieves all feedback for a specific product.
        [HttpGet("GetFeedbackForProduct/{productId}")]
        public async Task<ActionResult<ApiResponse<ProductFeedbackResponseDTO>>> GetFeedbackForProduct(int productId)
        {
            var response = await _feedBackService.GetFeedbackForProductAsync(productId);
            if (response.StatusCode != 200)
            {
                return StatusCode(response.StatusCode, response);
            }
            return Ok(response);
        }
        // Retrieves all feedback (Admin use).
        [HttpGet("GetAllFeedback")]
        public async Task<ActionResult<ApiResponse<List<FeedBackResponseDTO>>>> GetAllFeedback()
        {
            var response = await _feedBackService.GetAllFeedBacks();
            if (response.StatusCode != 200)
            {
                return StatusCode(response.StatusCode, response);
            }
            return Ok(response);
        }
        // Updates a specific feedback entry.
        [HttpPut("UpdateFeedback")]
        public async Task<ActionResult<ApiResponse<FeedBackResponseDTO>>> UpdateFeedback([FromBody] UpdateFeedBackDTO feedbackUpdateDTO)
        {
            var response = await _feedBackService.UpdateFeedbackAsync(feedbackUpdateDTO);
            if (response.StatusCode != 200)
            {
                return StatusCode(response.StatusCode, response);
            }
            return Ok(response);
        }
        // Deletes a specific feedback entry.
        [HttpDelete("DeleteFeedback")]
        public async Task<ActionResult<ApiResponse<ConfirmationResponseDTO>>> DeleteFeedback([FromBody] DeleteFeedBackDTO feedbackDeleteDTO)
        {
            var response = await _feedBackService.DeleteFeedbackAsync(feedbackDeleteDTO);
            if (response.StatusCode != 200)
            {
                return StatusCode(response.StatusCode, response);
            }
            return Ok(response);
        }
    }

    #endregion
}
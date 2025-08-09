using ECommerceApp.DTOs;
using ECommerceApp.DTOs.CustomerDTOs;
using ECommerceApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        #region Fields
        private readonly CustomerService _customerService;
        #endregion

        #region Constructor
        public CustomersController(CustomerService customerService)
        {
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
        }
        #endregion

        #region Actions

        [HttpPost("[Action]")]
        public async Task<ActionResult<ApiResponse<CustomerResponseDTO>>> CreateCustomer([FromBody] CustomerRegistrationDTO registrationDTO)
        {
            var response = await _customerService.RegisterCustomerAsync(registrationDTO);
            if (response.StatusCode != 200)
                return StatusCode((int)response.StatusCode, response);
            return Ok(response);
        }

        [HttpPost("[Action]")]
        public async Task<ActionResult<ApiResponse<LoginResponseDTO>>> LoginCustomer([FromBody] LoginDTO loginDTO)
        {
            var response = await _customerService.LoginAsync(loginDTO);
            if (response.StatusCode != 200)
                return StatusCode((int)response.StatusCode, response);
            return Ok(response);
        }

        [HttpGet("[Action]")]
        public async Task<ActionResult<ApiResponse<CustomerResponseDTO>>> GetCustomerDetails(int customerId)
        {
            var response = await _customerService.GetCustomerByIdAsync(customerId);
            if (response.StatusCode != 200)
                return StatusCode((int)response.StatusCode, response);
            return Ok(response);
        }

        [HttpPut("[Action]")]
        public async Task<ActionResult<ApiResponse<ConfirmationResponseDTO>>> UpdateCustomerDetails([FromBody] CustomerUpdateDTO updateDTO)
        {
            var response = await _customerService.UpdateCustomerAsync(updateDTO);
            if (response.StatusCode != 200)
                return StatusCode((int)response.StatusCode, response);
            return Ok(response);
        }

        [HttpDelete("[Action]")]
        public async Task<ActionResult<ApiResponse<ConfirmationResponseDTO>>> DeleteCustomer(int customerId)
        {
            var response = await _customerService.DeleteCustomerAsync(customerId);
            if (response.StatusCode != 200)
                return StatusCode((int)response.StatusCode, response);
            return Ok(response);
        }

        [HttpPost("[Action]")]
        public async Task<ActionResult<ApiResponse<ConfirmationResponseDTO>>> ChangePassword([FromBody] ChangePasswordDTO changePasswordDTO)
        {
            var response = await _customerService.ChangePasswordAsync(changePasswordDTO);
            if (response.StatusCode != 200)
                return StatusCode((int)response.StatusCode, response);
            return Ok(response);
        }
        #endregion
    }
}



using ECommerceApp.DTOs;
using ECommerceApp.DTOs.AddressesDTOs;
using ECommerceApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        #region Fields
        private readonly AddressService _addressService;
        #endregion
        #region Constructors
        public AddressesController(AddressService addressService)
        {
            _addressService = addressService;
        }
        #endregion

        #region Actions
        [HttpPost("[action]")]
        public async Task<ActionResult<ApiResponse<AddressResponseDTO>>> CreateAddress([FromBody] AddressCreateDTO addressCreateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _addressService.CreateAddreess(addressCreateDTO);
            if (response.StatusCode == 200)
            {
                return Ok(response);
            }
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<ApiResponse<AddressResponseDTO>>> GetAddressById(int id)
        {
            var response = await _addressService.GetAddressById(id);
            if (response.StatusCode == 200)
            {
                return Ok(response);
            }
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<ApiResponse<List<AddressResponseDTO>>>> GetAllAddressesByCustomerId(int id)
        {
            var response = await _addressService.GetAllAddressesByCustomerId(id);
            if (response.StatusCode == 200)
            {
                return Ok(response);
            }
            return StatusCode(response.StatusCode, response);
        }
        [HttpPut("[action]")]
        public async Task<ActionResult<ApiResponse<AddressResponseDTO>>> UpdateAddress([FromBody] AddressUpdateDTO addressUpdateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _addressService.UpdateAddressAsync(addressUpdateDTO);
            if (response.StatusCode == 200)
            {
                return Ok(response);
            }
            return StatusCode(response.StatusCode, response);
        }
        [HttpDelete("[action]/{id}")]
        public async Task<ActionResult<ApiResponse<ConfirmationResponseDTO>>> DeleteAddress(AddressDeleteDTO deleteDTO)
        {
            var response = await _addressService.DeleteAddressAsync(deleteDTO);
            if (response.StatusCode == 200)
            {
                return Ok(response);
            }
            return StatusCode(response.StatusCode, response);
        }


        #endregion

    }
}

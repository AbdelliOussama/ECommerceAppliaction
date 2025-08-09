using AutoMapper;
using ECommerceApp.Data;
using ECommerceApp.DTOs;
using ECommerceApp.DTOs.AddressesDTOs;
using ECommerceApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Services
{
    public class AddressService
    {
        #region Fields
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public AddressService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        #endregion

        #region Actions
        public async Task<ApiResponse<AddressResponseDTO>> CreateAddreess(AddressCreateDTO addressCreateDTO)
        {
            try
            {
                var customer = await _context.Customers.FindAsync(addressCreateDTO.CustomerId);
                if (customer == null)
                {
                    return new ApiResponse<AddressResponseDTO>(500, "Customer not found.");
                }
                var addrress = _mapper.Map<Address>(addressCreateDTO);
                _context.Addresses.Add(addrress);
                await _context.SaveChangesAsync();

                var addressResponse = _mapper.Map<AddressResponseDTO>(addrress);
                return new ApiResponse<AddressResponseDTO>(200, addressResponse);
            }
            catch (Exception ex)
            {
                return new ApiResponse<AddressResponseDTO>(500, $"An unexpected error occurred while processing your request, Error: {ex.Message}");
            }
        }

        public async Task<ApiResponse<AddressResponseDTO>> GetAddressById(int id)
        {
            try
            {
                var address = await _context.Addresses.AsNoTracking()
                    .FirstOrDefaultAsync(a => a.Id == id);
                if (address == null)
                {
                    return new ApiResponse<AddressResponseDTO>(404, "Address not found.");
                }
                var addressResponse = _mapper.Map<AddressResponseDTO>(address);
                return new ApiResponse<AddressResponseDTO>(200, addressResponse);
            }
            catch (Exception ex)
            {
                return new ApiResponse<AddressResponseDTO>(500, $"An unexpected error occurred while processing your request, Error: {ex.Message}");
            }
        }
        public async Task<ApiResponse<ConfirmationResponseDTO>> UpdateAddressAsync(AddressUpdateDTO addressUpdateDTO)
        {
            try
            {
                var address = await _context.Addresses.AsNoTracking()
                    .FirstOrDefaultAsync(a => a.Id == addressUpdateDTO.AddressId && a.CustomerId == addressUpdateDTO.CustomerId);
                if (address == null)
                {
                    return new ApiResponse<ConfirmationResponseDTO>(404, "Address not found.");
                }
                var addressToUpdate = _mapper.Map<Address>(addressUpdateDTO);
                _context.Addresses.Update(address);
                await _context.SaveChangesAsync();
                return new ApiResponse<ConfirmationResponseDTO>(200, new ConfirmationResponseDTO { Message = "Address updated successfully." });
            }
            catch (Exception ex)
            {
                return new ApiResponse<ConfirmationResponseDTO>(500, $"An unexpected error occurred while processing your request, Error: {ex.Message}");
            }
        }


        public async Task<ApiResponse<ConfirmationResponseDTO>> DeleteAddressAsync(AddressDeleteDTO addressDeleteDTO)
        {
            try
            {
                var address = await _context.Addresses.AsNoTracking()
                    .FirstOrDefaultAsync(a => a.Id == addressDeleteDTO.AddressId && a.CustomerId == addressDeleteDTO.CustomerId);
                if (address == null)
                {
                    return new ApiResponse<ConfirmationResponseDTO>(404, "Address not found.");
                }
                _context.Addresses.Remove(address);
                await _context.SaveChangesAsync();
                return new ApiResponse<ConfirmationResponseDTO>(200, new ConfirmationResponseDTO { Message = "Address deleted successfully." });
            }
            catch (Exception ex)
            {
                return new ApiResponse<ConfirmationResponseDTO>(500, $"An unexpected error occurred while processing your request, Error: {ex.Message}");
            }
        }

        public async Task<ApiResponse<List<AddressResponseDTO>>> GetAllAddressesByCustomerId(int customerId)
        {
            try
            {
                var customer = await _context.Customers.AsNoTracking()
                    .Include(c => c.Addresses)
                    .FirstOrDefaultAsync(c => c.Id == customerId);
                if (customer == null)
                    return new ApiResponse<List<AddressResponseDTO>>(404, "Customer not found.");
                var addresses = customer.Addresses;
                if (addresses == null || !addresses.Any())
                {
                    return new ApiResponse<List<AddressResponseDTO>>(404, "No addresses found for this customer.");
                }
                var addressResponses = _mapper.Map<List<AddressResponseDTO>>(addresses);
                return new ApiResponse<List<AddressResponseDTO>>(200, addressResponses);
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<AddressResponseDTO>>(500, $"An unexpected error occurred while processing your request, Error: {ex.Message}");
            }
        }

        #endregion
    }
}

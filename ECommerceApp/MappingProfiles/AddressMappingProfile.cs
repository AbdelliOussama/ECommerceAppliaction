using AutoMapper;
using ECommerceApp.DTOs.AddressesDTOs;
using ECommerceApp.Models;

namespace ECommerceApp.MappingProfiles
{
    public class AddressMappingProfile : Profile
    {
        public AddressMappingProfile()
        {
            // Map From AdressCreateDTO Ti Address
            CreateMap<AddressCreateDTO, Address>();

            // Map from Address to AddressResponseDTO
            CreateMap<Address, AddressResponseDTO>();
        }
    }
}

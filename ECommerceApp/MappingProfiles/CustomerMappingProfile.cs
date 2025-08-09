using AutoMapper;
using ECommerceApp.DTOs.CustomerDTOs;
using ECommerceApp.Models;

namespace ECommerceApp.MappingProfiles
{
    public class CustomerMappingProfile : Profile
    {
        public CustomerMappingProfile()
        {
            // Mapping CustomerDTO to Customer entity
            CreateMap<CustomerRegistrationDTO, Customer>()
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true)) // Default to active
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => BCrypt.Net.BCrypt.HashPassword(src.Password))); // Hash the password
                                                                                                                            //.ReverseMap();

            // Mapping Customer entity to CustomerDTO 
            CreateMap<Customer, CustomerResponseDTO>()
                .ReverseMap();

            //Mapping UpdateCustomerDTO to Customer entity
            CreateMap<CustomerUpdateDTO, Customer>();
        }
    }
}

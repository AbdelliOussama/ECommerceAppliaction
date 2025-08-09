using AutoMapper;
using ECommerceApp.DTOs.ProductDTOs;
using ECommerceApp.Models;

namespace ECommerceApp.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            // Mapping from ProductCreateDTO to Product
            CreateMap<ProductCreateDTO, Product>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // Ignore Id for creation
                .ForMember(dest => dest.IsAvailable, opt => opt.MapFrom(src => true)); // Set IsAvailable to true by default
            // Mapping from ProductUpdateDTO to Product
            CreateMap<ProductUpdateDTO, Product>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id)); // Map Id for update
            // Mapping from Product to ProductResponseDTO
            CreateMap<Product, ProductResponseDTO>();
        }
    }
}

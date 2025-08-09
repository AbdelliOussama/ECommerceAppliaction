using AutoMapper;
using ECommerceApp.DTOs.CategoryDTOs;
using ECommerceApp.Models;

namespace ECommerceApp.MappingProfiles
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            // Map From CategoryDTO to Category
            CreateMap<CategoryCreateDTO, Category>();
            // Map From Category To CategoryResponseDTO
            CreateMap<Category, CategoryResponseDTO>();
            // Map From CategoryUpdateDTO to Category
            CreateMap<CategoryUpdateDTO, Category>();


        }
    }
}

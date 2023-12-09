using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Dtos;
using DataLayer.Entities;

namespace DataLayer.Mapping
{
    public static class CategoryMappingExtension
    {
        public static CategoryDto ToDto(this Category category)
        {
            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public static List<CategoryDto> ToCategoryDtos(this Task<List<Category>> categories)
        {
            return categories.Result.Select(ToDto).ToList();
        } 
    }
}

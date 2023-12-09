using DataLayer;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Dtos;
using DataLayer.Mapping;


namespace Core.Services
{
    public class CategoryService
    {
        private readonly UnitOfWork _unitOfWork;

        public CategoryService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<CategoryDto>> GetAll()
        {
            var categories = _unitOfWork.Categories.GetAll();
            return categories.ToCategoryDtos();
        }
    }
}

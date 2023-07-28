using CourseMicroservices.Services.Catalog.Dtos.Category;
using CourseMicroservices.Shared.Dtos;

namespace CourseMicroservices.Services.Catalog.Interfaces
{
    public interface ICategoryService
    {
        Task<ResponseDto<List<CategoryDto>>> GetAllAsync();
        Task<ResponseDto<CreateCategoryDto>> CreateAsync(CreateCategoryDto createCategoryDto);
        Task<ResponseDto<CategoryDto>> GetByIdAsync(string id);
    }
}

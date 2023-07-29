using CourseMicroservices.Services.Catalog.Dtos.Course;
using CourseMicroservices.Shared.Dtos;

namespace CourseMicroservices.Services.Catalog.Interfaces
{
    public interface ICourseService
    {
        Task<ResponseDto<List<CourseDto>>> GetAllAsync();
        Task<ResponseDto<CourseDto>> GetByIdAsync(string id);
        Task<ResponseDto<List<CourseDto>>> GetAllByUserIdAsync(string userId);
        Task<ResponseDto<CreateCourseDto>> CreateAsync(CreateCourseDto createCourseDto);
        Task<ResponseDto<NoContentDto>> UpdateAsync(UpdateCourseDto UpdateCourseDto);
        Task<ResponseDto<NoContentDto>> DeleteAsync(string id);
    }
}

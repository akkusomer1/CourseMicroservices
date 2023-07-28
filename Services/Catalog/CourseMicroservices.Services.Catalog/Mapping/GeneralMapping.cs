using AutoMapper;
using CourseMicroservices.Services.Catalog.Dtos.Category;
using CourseMicroservices.Services.Catalog.Dtos.Course;
using CourseMicroservices.Services.Catalog.Dtos.Feature;
using CourseMicroservices.Services.Catalog.Models;

namespace CourseMicroservices.Services.Catalog.Mapping
{
    public class GeneralMapping:Profile
    {
        public GeneralMapping()
        {
            CreateMap<Course, CreateCourseDto>().ReverseMap();
            CreateMap<Course, UpdateCourseDto>().ReverseMap();
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CreateCategoryDto>().ReverseMap();
            CreateMap<Feature, FeatureDto>().ReverseMap();
        }
    }
}

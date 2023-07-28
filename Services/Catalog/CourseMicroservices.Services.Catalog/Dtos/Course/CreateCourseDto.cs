using CourseMicroservices.Services.Catalog.Dtos.Category;
using CourseMicroservices.Services.Catalog.Dtos.Feature;

namespace CourseMicroservices.Services.Catalog.Dtos.Course
{
    public class CreateCourseDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Picture { get; set; }
        public string UserId { get; set; }
        public string Description { get; set; }
        public FeatureDto Feature { get; set; }
        public string CategoryId { get; set; }
        public CategoryDto Category { get; set; }
    }
}

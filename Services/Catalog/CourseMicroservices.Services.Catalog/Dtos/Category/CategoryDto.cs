using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace CourseMicroservices.Services.Catalog.Dtos.Category
{
    public class CategoryDto
    {     
        public string Id { get; set; }
        public string Name { get; set; }
    }
}

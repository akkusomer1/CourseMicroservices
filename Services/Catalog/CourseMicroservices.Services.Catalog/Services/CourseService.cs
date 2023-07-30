using AutoMapper;
using CourseMicroservices.Services.Catalog.Dtos.Course;
using CourseMicroservices.Services.Catalog.Interfaces;
using CourseMicroservices.Services.Catalog.Models;
using CourseMicroservices.Services.Catalog.Settings;
using CourseMicroservices.Shared.Dtos;
using MongoDB.Driver;

namespace CourseMicroservices.Services.Catalog.Services
{
    public class CourseService : ICourseService
    {
        private readonly IMongoCollection<Course> _courseCollection;
        private readonly IMongoCollection<Category> _categoryCollection;

        private readonly IDatabaseSettnings _databaseSettnings;
        private readonly IMapper _mapper;

        public CourseService(IDatabaseSettnings databaseSettnings, IMapper mapper)
        {
            _databaseSettnings = databaseSettnings;
            var client = new MongoClient(_databaseSettnings!.ConnectionStrings);
            var database = client.GetDatabase(_databaseSettnings.DatabaseName);
            _courseCollection = database.GetCollection<Course>(_databaseSettnings.CourseCollectionName);
            _categoryCollection = database.GetCollection<Category>(_databaseSettnings.CategoryCollectionName);


            _mapper = mapper;
        }

        public async Task<ResponseDto<List<CourseDto>>> GetAllAsync()
        {
            var courses = await _courseCollection.Find(course => true).ToListAsync();

            if (courses.Any())
            {
                courses.ForEach(async x =>
                {
                    x.Category = await _categoryCollection.Find(c => c.Id == x.CategoryId).FirstOrDefaultAsync();

                });
            }
            else
            {
                courses = new List<Course>();
            }

            return ResponseDto<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
        }


        public async Task<ResponseDto<CourseDto>> GetByIdAsync(string id)
        {
            var course = await _courseCollection.Find<Course>(x => x.Id == id).FirstOrDefaultAsync();

            if (course is null)
            {
                return ResponseDto<CourseDto>.Fail("Course Not Found", 404);
            }

            course.Category = await _categoryCollection.Find<Category>(c => c.Id == course.CategoryId).FirstOrDefaultAsync();

            return ResponseDto<CourseDto>.Success(_mapper.Map<CourseDto>(course), 200);
        }


        public async Task<ResponseDto<List<CourseDto>>> GetAllByUserIdAsync(string userId)
        {

            var courses = await _courseCollection.Find(x => x.UserId == userId).ToListAsync();

            if (courses.Any())
            {
                courses.ForEach(async x =>
                {
                    x.Category = await _categoryCollection.Find(c => c.Id == x.CategoryId).FirstOrDefaultAsync();
                });
            }
            else
            {
                courses = new List<Course>();
            }

            return ResponseDto<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
        }


        public async Task<ResponseDto<CreateCourseDto>> CreateAsync(CreateCourseDto createCourseDto)
        {
            var course = _mapper.Map<Course>(createCourseDto);
            course.CreatedTime = DateTime.Now;
            await _courseCollection.InsertOneAsync(course);
            return ResponseDto<CreateCourseDto>.Success(createCourseDto, 200);
        }

        public async Task<ResponseDto<NoContentDto>> UpdateAsync(UpdateCourseDto UpdateCourseDto)
        {
            var course = _mapper.Map<Course>(UpdateCourseDto);


            await _courseCollection.FindOneAndReplaceAsync(x => x.Id == course.Id, course);

            if (course is null)
            {
                return ResponseDto<NoContentDto>.Fail("Course Not Found", 404);
            }
            return ResponseDto<NoContentDto>.Success(204);
        }


        public async Task<ResponseDto<NoContentDto>> DeleteAsync(string id)
        {
            var result = await _courseCollection.DeleteOneAsync(x => x.Id == id);

            if (result.DeletedCount > 0)
            {
                return ResponseDto<NoContentDto>.Success(204);
            }
            else
            {
                return ResponseDto<NoContentDto>.Fail("Course Not Found", 404);
            }
        }
    }
}

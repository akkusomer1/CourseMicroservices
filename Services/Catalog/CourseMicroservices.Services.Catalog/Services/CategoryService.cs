using AutoMapper;
using CourseMicroservices.Services.Catalog.Dtos.Category;
using CourseMicroservices.Services.Catalog.Interfaces;
using CourseMicroservices.Services.Catalog.Models;
using CourseMicroservices.Services.Catalog.Settings;
using CourseMicroservices.Shared.Dtos;
using MongoDB.Driver;
using SharpCompress.Compressors.Xz;
using System.Collections.Generic;

namespace CourseMicroservices.Services.Catalog.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Category> _categoriesCollection;
        private readonly IMapper _mapper;
        private readonly IDatabaseSettnings _databaseSettnings;

        public CategoryService(IMapper mapper, IDatabaseSettnings databaseSettnings)
        {

            _mapper = mapper;
            _databaseSettnings = databaseSettnings;

            //1-MongoDb'ye bağlan.
            var client = new MongoClient(_databaseSettnings.ConnectionStrings);

            //2-Database oluşturç

            var database = client.GetDatabase(_databaseSettnings.DatabaseName);

            //Bu database üzerindende bana bir collection yani Table ver diyorum.
            _categoriesCollection = database.GetCollection<Category>(databaseSettnings.CategoryCollectionName);
        }

        public async Task<ResponseDto<List<CategoryDto>>> GetAllAsync()
        {
            var categories = await _categoriesCollection.Find(category => true).ToListAsync();
            var mappedCategories = _mapper.Map<List<CategoryDto>>(categories);
            return ResponseDto<List<CategoryDto>>.Success(mappedCategories, 200);
        }

        public async Task<ResponseDto<CreateCategoryDto>> CreateAsync(CreateCategoryDto createCategoryDto)
        {
            var category = _mapper.Map<Category>(createCategoryDto);
            await _categoriesCollection.InsertOneAsync(category);
            return ResponseDto<CreateCategoryDto>.Success(createCategoryDto, 201);
        }

        public async Task<ResponseDto<CategoryDto>> GetByIdAsync(string id)
        {
            Category category = await _categoriesCollection.Find<Category>(x => x.Id == id).FirstOrDefaultAsync();

            if (category == null)
                return ResponseDto<CategoryDto>.Fail("Category Not Found", 404);

            var categoryDtoMapped = _mapper.Map<CategoryDto>(category);
            return ResponseDto<CategoryDto>.Success(categoryDtoMapped, 200);
        }
    }
}

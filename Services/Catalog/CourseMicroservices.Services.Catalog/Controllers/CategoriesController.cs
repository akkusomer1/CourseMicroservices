using CourseMicroservices.Services.Catalog.Dtos.Category;
using CourseMicroservices.Services.Catalog.Dtos.Course;
using CourseMicroservices.Services.Catalog.Interfaces;
using CourseMicroservices.Shared.ControllerBases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseMicroservices.Services.Catalog.Controllers
{
    public class CategoriesController:BaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        
        [HttpGet]
        public async Task<IActionResult> GetAll()=>
             CreateActionResult(await _categoryService.GetAllAsync());
        

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)=>
             CreateActionResult(await _categoryService.GetByIdAsync(id));

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryDto createCategoryDto)=>     
             CreateActionResult(await _categoryService.CreateAsync(createCategoryDto));       
    }
}

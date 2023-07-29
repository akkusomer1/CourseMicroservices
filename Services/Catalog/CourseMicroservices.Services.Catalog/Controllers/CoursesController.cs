using CourseMicroservices.Services.Catalog.Dtos.Course;
using CourseMicroservices.Services.Catalog.Interfaces;
using CourseMicroservices.Shared.ControllerBases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseMicroservices.Services.Catalog.Controllers
{
    public class CoursesController : BaseController
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return CreateActionResult(await _courseService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            return CreateActionResult(await _courseService.GetByIdAsync(id));
        }


        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAllByUserId(string userId)
        {
            return CreateActionResult(await _courseService.GetAllByUserIdAsync(userId));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCourseDto createCourseDto)
        {
            return CreateActionResult(await _courseService.CreateAsync(createCourseDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCourseDto updateCourseDto)
        {
            return CreateActionResult(await _courseService.UpdateAsync(updateCourseDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return CreateActionResult(await _courseService.DeleteAsync(id));
        }

    }
}

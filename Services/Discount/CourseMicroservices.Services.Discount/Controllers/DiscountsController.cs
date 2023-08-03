using CourseMicroservices.Services.Discount.Interfaces;
using CourseMicroservices.Services.Discount.Models;
using CourseMicroservices.Shared.ControllerBases;
using CourseMicroservices.Shared.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseMicroservices.Services.Discount.Controllers
{
    [Authorize]
    public class DiscountsController : BaseController
    {
        private readonly IDiscountService _discountService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public DiscountsController(IDiscountService discountService, ISharedIdentityService sharedIdentityService)
        {
            _discountService = discountService;
            _sharedIdentityService = sharedIdentityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return CreateActionResult(await _discountService.GetAllAsync());
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return CreateActionResult(await _discountService.DeleteAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(DiscountModel discount)
        {
            return CreateActionResult(await _discountService.SaveAsync(discount));
        }

        [HttpPut]
        public async Task<IActionResult> Update(DiscountModel discount)
        {
            return CreateActionResult(await _discountService.UpdateAsync(discount));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return CreateActionResult(await _discountService.GetByIdAsync(id));
        }

        [HttpGet("[action]/{code}")]
        public async Task<IActionResult> GetByCodeAndUserId(string code)
        {
            return CreateActionResult(await _discountService.GetByCodeAndUserIdAsync(code, _sharedIdentityService.GetUserId));
        }
    }
}

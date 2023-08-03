using CourseMicroservices.Services.Basket.DTOs;
using CourseMicroservices.Services.Basket.Interfaces;
using CourseMicroservices.Shared.ControllerBases;
using CourseMicroservices.Shared.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseMicroservices.Services.Basket.Controllers
{
    [Authorize]
    public class BasketsController : BaseController
    {

        private readonly IBasketService _basketService; 
        private readonly ISharedIdentityService _sharedIdentityService;

        public BasketsController(IBasketService basketService, ISharedIdentityService sharedIdentityService)
        {
            _basketService = basketService;
            _sharedIdentityService = sharedIdentityService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdate(BasketDto basketDto)
        {
            return CreateActionResult(await _basketService.SaveOrUpdateAsync(basketDto));        
        }

        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            return CreateActionResult(await _basketService.DeleteAsync(_sharedIdentityService.GetUserId));
        }


        [HttpGet]
        public async Task<IActionResult> GetBasket()
        {
            return CreateActionResult(await _basketService.GetBasketAsync(_sharedIdentityService.GetUserId));
        }
    }
}

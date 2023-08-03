using CourseMicroservices.Services.Basket.DTOs;
using CourseMicroservices.Services.Basket.Interfaces;
using CourseMicroservices.Shared.Dtos;
using CourseMicroservices.Shared.Interfaces;
using CourseMicroservices.Shared.Services;
using Microsoft.AspNetCore.Authorization;
using StackExchange.Redis;
using System.Text.Json;

namespace CourseMicroservices.Services.Basket.Services
{
    public class BasketService : IBasketService
    {
        private readonly RedisService _redisService;
        private readonly IDatabase _db;
        private readonly ISharedIdentityService _sharedIdentityService;


        public BasketService(RedisService redisService, ISharedIdentityService sharedIdentityService)
        {
            _redisService = redisService;
            _sharedIdentityService = sharedIdentityService;
            _db = _redisService.GetDb();
        }

        public async Task<ResponseDto<bool>> DeleteAsync(string userId)
        {
            var result =await _db.KeyDeleteAsync(userId);
            return result ? ResponseDto<bool>.Success(true,200) : ResponseDto<bool>.Fail("Basket Not Found", 404);
        }

        public async Task<ResponseDto<BasketDto>> GetBasketAsync(string userId)
        {
            var basket = await _db.StringGetAsync(userId);

            if (string.IsNullOrEmpty(basket))
                return ResponseDto<BasketDto>.Fail("Basket Not Found", 404);

            return ResponseDto<BasketDto>.Success(JsonSerializer.Deserialize<BasketDto>(basket!)!, 200);



        }
        public async Task<ResponseDto<bool>> SaveOrUpdateAsync(BasketDto basketDto)
        {
            basketDto.UserId=_sharedIdentityService.GetUserId;
            var result = await _db.StringSetAsync(basketDto.UserId, JsonSerializer.Serialize(basketDto));
            
            return result ? ResponseDto<bool>.Success(true,200) : ResponseDto<bool>.Fail("Basket could not update or save", 500);
        }
    }
}

using CourseMicroservices.Services.Basket.DTOs;
using CourseMicroservices.Services.Basket.Interfaces;
using CourseMicroservices.Shared.Dtos;
using StackExchange.Redis;
using System.Text.Json;

namespace CourseMicroservices.Services.Basket.Services
{
    public class BasketService : IBasketService
    {
        private readonly RedisService _redisService;
        private readonly IDatabase _db;
        public BasketService(RedisService redisService)
        {
            _redisService = redisService;
            _db = _redisService.GetDb();
        }

        public async Task<ResponseDto<bool>> Delete(string userId)
        {
            var result =await _db.KeyDeleteAsync(userId);
            return result ? ResponseDto<bool>.Success(200) : ResponseDto<bool>.Fail("Basket Not Found", 404);

        }

        public async Task<ResponseDto<BasketDto>> GetBasket(string userId)
        {
            var basket = await _db.StringGetAsync(userId);

            if (string.IsNullOrEmpty(basket))
                return ResponseDto<BasketDto>.Fail("Basket Not Found", 404);

            return ResponseDto<BasketDto>.Success(JsonSerializer.Deserialize<BasketDto>(basket!)!, 200);



        }

        //StringSet metodu ile hem update hemde Save işlemi yapacağız eğer ilgili key varsa update yapar yoksa yeni save yapar.
        public async Task<ResponseDto<bool>> SaveOrUpdate(BasketDto basketDto)
        {
            var result = await _db.StringSetAsync(basketDto.UserId, JsonSerializer.Serialize(basketDto));

            return result ? ResponseDto<bool>.Success( 200) : ResponseDto<bool>.Fail("Basket could not update or save", 500);
        }
    }
}

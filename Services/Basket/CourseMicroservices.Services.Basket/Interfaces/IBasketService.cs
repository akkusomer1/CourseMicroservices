using CourseMicroservices.Services.Basket.DTOs;
using CourseMicroservices.Shared.Dtos;

namespace CourseMicroservices.Services.Basket.Interfaces
{
    public interface IBasketService
    {
        Task<ResponseDto<BasketDto>> GetBasketAsync(string userId);
        Task<ResponseDto<bool>> SaveOrUpdateAsync(BasketDto basketDto);
        Task<ResponseDto<bool>> DeleteAsync(string userId);
    }
}

using CourseMicroservices.Services.Basket.DTOs;
using CourseMicroservices.Shared.Dtos;

namespace CourseMicroservices.Services.Basket.Interfaces
{
    public interface IBasketService
    {
        Task<ResponseDto<BasketDto>> GetBasket(string userId);
        Task<ResponseDto<bool>> SaveOrUpdate(BasketDto basketDto);
        Task<ResponseDto<bool>> Delete(string userId);
    }
}

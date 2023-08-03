using CourseMicroservices.Services.Discount.Models;
using CourseMicroservices.Shared.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CourseMicroservices.Services.Discount.Interfaces
{
    public interface IDiscountService
    {
        Task<ResponseDto<List<DiscountModel>>> GetAllAsync();
        Task<ResponseDto<DiscountModel>> GetByIdAsync(int id);
        Task<ResponseDto<NoContent>> SaveAsync(DiscountModel discount);
        Task<ResponseDto<NoContent>> UpdateAsync(DiscountModel discount);
        Task<ResponseDto<NoContent>> DeleteAsync(int id);
        Task<ResponseDto<DiscountModel>> GetByCodeAndUserIdAsync(string code, string userld);
    }
}

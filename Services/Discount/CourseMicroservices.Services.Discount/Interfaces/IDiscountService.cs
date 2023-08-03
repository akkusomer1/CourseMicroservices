using CourseMicroservices.Services.Discount.Models;
using CourseMicroservices.Shared.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CourseMicroservices.Services.Discount.Interfaces
{
    public interface IDiscountService
    {
        Task<ResponseDto<List<DiscountModel>>> GetAll();
        Task<ResponseDto<DiscountModel>> GetById(int id);
        Task<ResponseDto<NoContent>> Save(DiscountModel discount);
        Task<ResponseDto<NoContent>> Update(DiscountModel discount);
        Task<ResponseDto<NoContent>> Delete(int id);
        Task<ResponseDto<DiscountModel>> GetByCodeAndUserId(string code, string userld);
    }
}

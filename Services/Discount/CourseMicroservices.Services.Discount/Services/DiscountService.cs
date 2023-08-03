using CourseMicroservices.Services.Discount.Interfaces;
using CourseMicroservices.Services.Discount.Models;
using CourseMicroservices.Shared.Dtos;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Npgsql;
using System.Collections.Generic;
using System.Data;

namespace CourseMicroservices.Services.Discount.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _dbConnection;

        public DiscountService(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("NpgLocalDb"));
        }

        public async Task<ResponseDto<NoContent>> DeleteAsync(int id)
        {
            var discount = await _dbConnection.QuerySingleOrDefaultAsync<DiscountModel>("select * from discount where id=@Id", new { Id = id });

            if (discount == null)
                return ResponseDto<NoContent>.Fail("Discount Not Found", 404);

            var result = await _dbConnection.ExecuteAsync("Delete from discount where id=@Id", new { Id = id });

            return result > 0 ? ResponseDto<NoContent>.Success(200) : ResponseDto<NoContent>.Fail("an error", 500);
        }


        public async Task<ResponseDto<List<DiscountModel>>> GetAllAsync()
        {
            var discounts = await _dbConnection.QueryAsync<DiscountModel>("select * from discount");

            return ResponseDto<List<DiscountModel>>.Success(discounts.ToList(), 200);
        }

        public async Task<ResponseDto<DiscountModel>> GetByCodeAndUserIdAsync(string code, string userId)
        {

            var discount = await _dbConnection.QueryAsync<DiscountModel>("select * from discount where userid=@UserId and code=@Code", new
            {
                UserId = userId,
                Code = code
            });
            var hasDiscount = discount.FirstOrDefault();

            if (hasDiscount == null)
                return ResponseDto<DiscountModel>.Fail("Discount Not Found", 404);

            return ResponseDto<DiscountModel>.Success(hasDiscount, 200);

        }

        public async Task<ResponseDto<DiscountModel>> GetByIdAsync(int id)
        {
            var discount = await _dbConnection.QuerySingleOrDefaultAsync<DiscountModel>("select * from discount where Id=@id", new { Id = id });

            if (discount == null)
                return ResponseDto<DiscountModel>.Fail("Discount Not Found", 404);

            return ResponseDto<DiscountModel>.Success(discount, 200);
        }

        public async Task<ResponseDto<NoContent>> SaveAsync(DiscountModel discount)
        {
            var insertStatus = await _dbConnection.ExecuteAsync("INSERT INTO discount(userid,rate,code) Values(@UserId,@Rate,@Code)",discount);

            if (insertStatus > 0)
                return ResponseDto<NoContent>.Success(204);

            return ResponseDto<NoContent>.Fail("an error accured while adding", 500);
        }

        public async Task<ResponseDto<NoContent>> UpdateAsync(DiscountModel discount)
        {
            var updateStatus = await _dbConnection.ExecuteAsync("Update discount set userid=@UserId,rate=@Rate,code=@Code where id=@Id", new
            {
                Id = discount.Id,
                UserId = discount.UserId,
                Rate = discount.Rate,
                Code = discount.Code,
            });

            if (updateStatus > 0)
                return ResponseDto<NoContent>.Success(204);

            return ResponseDto<NoContent>.Fail("Discoun Not Found", 404);
        }
    }
}

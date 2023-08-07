using CourseMicroservices.Services.Order.Application.DTOs;
using CourseMicroservices.Shared.Dtos;
using MediatR;

namespace CourseMicroservices.Services.Order.Application.Queries.Order
{
    public class GetOrderByUserIdQuery:IRequest<ResponseDto<List<OrderDto>>>
    {
        public string UserId { get; set; }
    }
}

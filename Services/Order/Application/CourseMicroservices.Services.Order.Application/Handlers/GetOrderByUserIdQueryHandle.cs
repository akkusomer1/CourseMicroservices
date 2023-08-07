using AutoMapper;
using CourseMicroservices.Services.Order.Application.DTOs;
using CourseMicroservices.Services.Order.Application.Queries.Order;
using CourseMicroservices.Services.Order.Instrastructure;
using CourseMicroservices.Shared.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CourseMicroservices.Services.Order.Application.Handlers
{
    public class GetOrderByUserIdQueryHandle : IRequestHandler<GetOrderByUserIdQuery, ResponseDto<List<OrderDto>>>
    {
        private readonly IMapper _mapper;
        private readonly OrderDbContext _context;

        public GetOrderByUserIdQueryHandle(IMapper mapper, OrderDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ResponseDto<List<OrderDto>>> Handle(GetOrderByUserIdQuery request, CancellationToken cancellationToken)
        {
            var orders = await _context.Orders.Where(x => x.BuyerId == request.UserId).ToListAsync();

            if (!orders.Any())
            {
                return ResponseDto<List<OrderDto>>.Success(new List<OrderDto>(), 200);
            }
            return ResponseDto<List<OrderDto>>.Success(_mapper.Map<List<OrderDto>>(orders), 200);
        }
    }
}

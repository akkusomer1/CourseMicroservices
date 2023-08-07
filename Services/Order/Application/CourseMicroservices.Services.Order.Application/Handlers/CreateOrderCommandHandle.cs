using CourseMicroservices.Services.Order.Application.Commands;
using CourseMicroservices.Services.Order.Application.DTOs;
using CourseMicroservices.Services.Order.Domain.OrderAggregate;
using CourseMicroservices.Services.Order.Instrastructure;
using CourseMicroservices.Shared.Dtos;
using MediatR;
using aliesOrder = CourseMicroservices.Services.Order.Domain.OrderAggregate.FreeCourse.Services.Order.Domain.OrderAggregate;

namespace CourseMicroservices.Services.Order.Application.Handlers
{
    public class CreateOrderCommandHandle : IRequestHandler<CreateOrderCommand, ResponseDto<CreatedOrderDto>>
    {
        private readonly OrderDbContext _context;
        public async Task<ResponseDto<CreatedOrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var newAddress = new Address(request.Address.Province, request.Address.District, request.Address.Street, request.Address.ZipCode, request.Address.Line);

            aliesOrder.Order newOrder = new(request.BuyerId, newAddress);

            request.OrderItems.ForEach(x =>
            {
                newOrder.AddOrderItem(x.ProductId, x.ProductName, x.Price, x.PictureUrl);
            });

            await _context.Orders.AddAsync(newOrder);

            await _context.SaveChangesAsync();

            return ResponseDto<CreatedOrderDto>.Success(new CreatedOrderDto { OrderId = newOrder.Id }, 200);
        }
    }
}

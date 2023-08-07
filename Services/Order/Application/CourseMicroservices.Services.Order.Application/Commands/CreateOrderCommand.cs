using Azure;
using CourseMicroservices.Services.Order.Application.DTOs;
using CourseMicroservices.Shared.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseMicroservices.Services.Order.Application.Commands
{
    public class CreateOrderCommand : IRequest<ResponseDto<CreatedOrderDto>>
    {
        public string BuyerId { get; set; }

        public List<OrderItemDto> OrderItems { get; set; }

        public AddressDto Address { get; set; }
    }
}

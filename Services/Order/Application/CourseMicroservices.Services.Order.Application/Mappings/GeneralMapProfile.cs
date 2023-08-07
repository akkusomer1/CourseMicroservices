using AutoMapper;
using CourseMicroservices.Services.Order.Application.DTOs;
using orderAlies= CourseMicroservices.Services.Order.Domain.OrderAggregate.FreeCourse.Services.Order.Domain.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseMicroservices.Services.Order.Domain.OrderAggregate;

namespace CourseMicroservices.Services.Order.Application.Mappings
{
    public class GeneralMapProfile:Profile
    {
        public GeneralMapProfile()
        {
            CreateMap<orderAlies.Order, OrderDto>().ReverseMap();
            CreateMap<OrderItem, OrderItemDto>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}

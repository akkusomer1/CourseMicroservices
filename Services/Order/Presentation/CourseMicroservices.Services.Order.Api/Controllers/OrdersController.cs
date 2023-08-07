using CourseMicroservices.Services.Order.Application.Commands;
using CourseMicroservices.Services.Order.Application.Queries.Order;
using CourseMicroservices.Shared.ControllerBases;
using CourseMicroservices.Shared.Dtos;
using CourseMicroservices.Shared.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseMicroservices.Services.Order.Api.Controllers
{
    [Authorize]
    public class OrdersController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly ISharedIdentityService _sharedIdentityService;

        public OrdersController(IMediator mediator, ISharedIdentityService sharedIdentityService)
        {
            _mediator = mediator;
            _sharedIdentityService = sharedIdentityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderByUserId()
        {
            var response = await _mediator.Send(new GetOrderByUserIdQuery { UserId = _sharedIdentityService.GetUserId });
            return CreateActionResult(response);
        }


        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderCommand command)
        {
            var response = await _mediator.Send(command);
            return CreateActionResult(response);
        }
    }
}

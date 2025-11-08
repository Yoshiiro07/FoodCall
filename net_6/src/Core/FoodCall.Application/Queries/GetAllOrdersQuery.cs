using System;
using FoodCall.Application.DTOs;
using MediatR;

namespace FoodCall.Application.Queries
{
    public class GetAllOrdersQuery : IRequest<IEnumerable<OrderDto>>
    {
        public GetAllOrdersQuery()
        {
        }
    }
}
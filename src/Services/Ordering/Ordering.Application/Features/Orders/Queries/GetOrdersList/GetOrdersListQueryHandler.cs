using AutoMapper;
using MediatR;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Models;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersList;

public class GetOrdersListQueryHandler : IRequestHandler<GetOrdersListQuery, List<OrdersVM>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetOrdersListQueryHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        this._mapper = mapper;
        this._orderRepository = orderRepository;

    }
    public Task<List<OrdersVM>> Handle(GetOrdersListQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

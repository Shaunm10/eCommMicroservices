using AutoMapper;
using MediatR;
using Ordering.Application.Contracts.Persistence;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersList;

public class GetOrdersListQueryHandler : IRequestHandler<GetOrdersListQuery, List<OrderVM>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetOrdersListQueryHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        this._orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
    }

    public async Task<List<OrderVM>> Handle(GetOrdersListQuery request, CancellationToken cancellationToken)
    {
        var orders = await this._orderRepository.GetOrdersByUserName(request.UserName);
        var ordersVM = this._mapper.Map<List<OrderVM>>(orders);

        return ordersVM;
    }
}

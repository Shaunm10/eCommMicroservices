using MediatR;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersList
{
    public class GetOrdersListQueryHandler : IRequestHandler<GetOrdersListQuery, List<OrdersVM>>
    {
        public Task<List<OrdersVM>> Handle(GetOrdersListQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
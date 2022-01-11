using MediatR;
using Ordering.Application.Models;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersList;

public class GetOrdersListQuery : IRequest<List<OrderVM>>
{
    public string UserName { get; set; }

    public GetOrdersListQuery(string userName)
    {
        this.UserName = userName ?? throw new ArgumentNullException(nameof(userName));
    }
}

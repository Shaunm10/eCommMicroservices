using MediatR;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersList;

public class GetOrdersListQueries : IRequest
{
    public string UserName { get; set; }

    public GetOrdersListQueries(string userName)
    {
        this.UserName = userName ?? throw new ArgumentNullException(nameof(userName));
    }

}

using AspnetRunBasics.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspnetRunBasics.Services
{
    public class OrderService : IOrderService
    {
        public OrderService()
        {

        }
        public Task<IEnumerable<OrderResponseModel>> GetOrderByUserNameAsync(string userName)
        {
            throw new System.NotImplementedException();
        }
    }
}

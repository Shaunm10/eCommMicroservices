﻿using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
        {
            // if there are no records yet.
            if (!orderContext.Orders.Any())
            {
                orderContext.Orders.AddRange();
            }
        }

        private static IEnumerable<Order> GetPreconfiguredOrders()
        {
            return new List<Order>
            {
                new Order
                {
                    UserName = "sms",
                    FirstName = "Shaun",
                    LastName = "Samsus",
                    EmailAddress = "samsus@example.com",
                    AddressLine = "373 Maple Ave",
                    Country = "Brazil",
                    TotalPrice = 868.57M
                }
            };

        }
    }
}

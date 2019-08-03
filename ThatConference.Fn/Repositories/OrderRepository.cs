using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ThatConference.Fn.Models;

namespace ThatConference.Fn.Repositories
{
    public class OrderRepository : IOrderRepository 
    {
        public async Task SaveAsync(Order order)
        {
            
        }

        public Task<Order> GetOrderByIdAsync(int orderId)
        {
            return Task.FromResult(new Order
            {
                OrderId = orderId
            });
        }
    }
}

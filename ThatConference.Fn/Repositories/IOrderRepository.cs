using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ThatConference.Fn.Models;

namespace ThatConference.Fn.Repositories
{
    public interface IOrderRepository
    {
        Task SaveAsync(Order order);
        Task<Order> GetOrderByIdAsync(int orderId);
    }
}

using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Build.Framework;
using Microsoft.Extensions.Logging;
using ThatConference.Fn.Models;
using ThatConference.Fn.Models.Request;
using ThatConference.Fn.Models.Response;
using ThatConference.Fn.Repositories;

namespace ThatConference.Fn.Services
{
    public class OrderService : IOrderService
    {
        #region Private Fields
        private ILogger<OrderService> _logger;
        private IOrderRepository _orderRepository;
        #endregion

        #region Constructor
        public OrderService(IOrderRepository orderRepository, ILogger<OrderService> logger)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        #endregion

        #region Public Methods
        public async Task SubmitOrderAsync(SubmitOrderRequest req)
        {
            if (req == null)
            {
                throw new ArgumentNullException(nameof(req));
            }

            await _orderRepository.SaveAsync(req.ToOrder());
        }

        public async Task<Order> GetOrderAsync(GetOrderRequest req)
        {
            var order = await _orderRepository.GetOrderByIdAsync(req.OrderId);
            return order;
        }

        #endregion
    }
}

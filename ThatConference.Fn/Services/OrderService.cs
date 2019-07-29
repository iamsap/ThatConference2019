using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Build.Framework;
using Microsoft.Extensions.Logging;
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
            _logger.LogInformation($"Submitting Order# {req.OrderId}");
        }
        #endregion
    }
}

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Build.Framework;
using Microsoft.Extensions.Logging;
using ThatConference.Fn.Models.Request;
using ThatConference.Fn.Services;

namespace ThatConference.Fn
{
    public class OrderFunctions
    {
        #region Private Fields
        private ILogger<OrderFunctions> _logger;
        private IOrderService _orderService;
        #endregion

        #region Constructor
        public OrderFunctions(IOrderService orderService, ILogger<OrderFunctions> logger)
        {
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        #endregion

        #region Submit Order

        [FunctionName("SubmitOrder")]
        public async Task<IActionResult> SubmitOrderAsync(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "order")]SubmitOrderRequest req)
        {
            await _orderService.SubmitOrderAsync(req);
            return new AcceptedResult();
        }
        #endregion

    }
}
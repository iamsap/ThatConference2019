using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using ThatConference.Fn.Models.Request;
using ThatConference.Fn.Services;

namespace ThatConference.Fn.Tests.Functions
{
    [TestClass]
    public class OrderFunctionsTests
    {
        private Mock<ILogger<OrderFunctions>> _logger;
        private Mock<IOrderService> _orderService;
        private OrderFunctions _orderFunctions;

        [TestInitialize]
        public void Init()
        {
            _logger = new Mock<ILogger<OrderFunctions>>();
            _orderService = new Mock<IOrderService>();
            _orderFunctions = new OrderFunctions(_orderService.Object, _logger.Object);
        }

        [TestMethod]
        public async Task Calls_SubmitOrderAsync()
        {
            // Arrange
            var msg = new HttpRequestMessage(HttpMethod.Post, "api/order");
            msg.Headers.Add("Authorization", new List<string>() { "Basic" });
            msg.Content = new StringContent(JsonConvert.SerializeObject(new SubmitOrderRequest()), Encoding.UTF8, "application/json");
            // Act
            var res = await _orderFunctions.SubmitOrderAsync(msg);
            // Assert
            _orderService.Verify(m => m.SubmitOrderAsync(It.IsAny<SubmitOrderRequest>()), Times.Once);
        }
    }
}

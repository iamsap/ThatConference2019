using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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
            var req = new SubmitOrderRequest();
            // Act
            var res = await _orderFunctions.SubmitOrderAsync(req);
            // Assert
            _orderService.Verify(m => m.SubmitOrderAsync(It.IsAny<SubmitOrderRequest>()), Times.Once);
        }
    }
}

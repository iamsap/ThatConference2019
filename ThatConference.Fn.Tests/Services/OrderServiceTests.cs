using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ThatConference.Fn.Models;
using ThatConference.Fn.Models.Request;
using ThatConference.Fn.Repositories;
using ThatConference.Fn.Services;

namespace ThatConference.Fn.Tests.Services
{
    [TestClass]
    public class OrderServiceTests
    {
        private ILogger<OrderService> _logger;
        private Mock<IOrderRepository> _orderRepository;
        private OrderService _orderService;

        [TestInitialize]
        public void Init()
        {
            _orderRepository = new Mock<IOrderRepository>();
            _logger = Mock.Of<ILogger<OrderService>>();
            _orderService = new OrderService(_orderRepository.Object, _logger);
        }

        [TestMethod]
        public async Task Handles_Null_Order()
        {
            // Arrange
            _orderRepository.Setup(m => m.GetOrderByIdAsync(It.IsAny<int>())).ReturnsAsync((Order)null);
            // Act
            Func<Task> func = async() => await _orderService.GetOrderAsync(new GetOrderRequest());
            // Assert
            func.Should().NotThrow<NullReferenceException>();
        }
    }
}
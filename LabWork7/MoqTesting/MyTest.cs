using FluentAssertions;
using Moq;
using OrderProcessing.Models;

namespace MoqTesting
{
    public class MyTest(RepositoryFixture fixture) : IClassFixture<RepositoryFixture>
    {
        private readonly RepositoryFixture _fixture = fixture;

        [Fact]
        public async Task CreateOrderAsyncWithCorrectCustomerCreatesOrder()
        {
            var order = await _fixture.OrderService.CreateOrderAsync(_fixture.CustomerId, 69.9m);

            order.TotalAmount.Should().Be(69.9m);
            order.CustomerId.Should().Be(_fixture.CustomerId);
        }

        [Fact]
        public async Task CreateOrderAsyncWithIncorrectCustomerThrowsException()
        {
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await _fixture.OrderService.CreateOrderAsync(Guid.NewGuid(), 69.9m));
        }

        [Fact]
        public async Task ConfirmPaymentAsyncWithCorrectOrderConfirmingOrder()
        {
            await _fixture.OrderService.ConfirmPaymentAsync(_fixture.Order.Id);

            _fixture.Order.Status.Should().Be(OrderStatus.Paid);
        }

        [Fact]
        public async Task ConfirmPaymentAsyncWithIncorrectOrderThrowsException()
        {
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await _fixture.OrderService.ConfirmPaymentAsync(Guid.NewGuid()));
        }

        [Fact]
        public async Task CancelOrderAsyncWithCorrectOrderCancelingOrder()
        {
            var order = _fixture.Order;

            await _fixture.OrderService.CancelOrderAsync(order.Id);

            order.Status.Should().Be(OrderStatus.Cancelled);
        }

        [Fact]
        public async Task CancelOrderAsyncWithIncorrectOrderThrowsException()
        {
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await _fixture.OrderService.CancelOrderAsync(Guid.NewGuid()));
        }

        [Fact]
        public async Task ShipOrderAsyncWithPaidOrderShippingOrder()
        {
            var order = _fixture.Order;
            order.Status = OrderStatus.Paid;
            _fixture.MockOrdersRepository.Setup(r => r.GetByIdAsync(order.Id)).ReturnsAsync(order);

            await _fixture.OrderService.ShipOrderAsync(order.Id);

            order.Status.Should().Be(OrderStatus.Shipped);
        }

        [Fact]
        public async Task ShipOrderAsyncWithNotPaidOrderThrowsException()
        {
            _fixture.Order.Status = OrderStatus.Pending;

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await _fixture.OrderService.ShipOrderAsync(_fixture.Order.Id));
        }

        [Fact]
        public async Task ShipOrderAsyncWithIncorrectOrderThrowsException()
        {
            var missingOrderId = Guid.NewGuid();

            Func<Task> act = () => _fixture.OrderService.ShipOrderAsync(missingOrderId);

            await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("Заказ не найден.");
        }
    }
}
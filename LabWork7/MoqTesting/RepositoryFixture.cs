using Moq;
using OrderProcessing.Interfaces;
using OrderProcessing.Models;
using OrderProcessing.Services;

namespace MoqTesting
{
    public class RepositoryFixture : IDisposable
    {
        public Mock<ICustomerRepository> MockCustomerRepository = new();
        public Mock<IMessageBus> MockMessageBus = new();
        public Mock<IOrderRepository> MockOrdersRepository = new();

        public Order Order { get; set; }
        public Guid CustomerId { get; set; }
        public OrderService OrderService { get; set; }


        public RepositoryFixture()
        {
            CustomerId = Guid.NewGuid();

            Order = new()
            {
                Id = Guid.NewGuid(),
                CustomerId = CustomerId,
            };

            MockCustomerRepository.Setup(c => c.GetByIdAsync(CustomerId)).ReturnsAsync(new Customer { Id = CustomerId });

            OrderService = new(MockOrdersRepository.Object, MockCustomerRepository.Object, MockMessageBus.Object);

            MockMessageBus.Setup(b => b.PublishAsync(It.IsAny<string>(), It.IsAny<object>()));

            MockOrdersRepository.Setup(r => r.AddAsync(It.IsAny<Order>()));
            MockOrdersRepository.Setup(r => r.UpdateAsync(It.IsAny<Order>()));
            MockOrdersRepository.Setup(r => r.GetByIdAsync(Order.Id)).ReturnsAsync(Order);
        }

        public void Dispose()
        {
        }
    }
}

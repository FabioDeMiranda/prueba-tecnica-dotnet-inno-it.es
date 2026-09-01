using InterviewTest.Application.Orders.Commands.CreateOrder;
using InterviewTest.Domain.Entities;
using InterviewTest.Domain.Events;
using InterviewTest.Infrastructure.Persistence.Repositories;
using InterviewTest.Tests.Support;
using Xunit;

namespace InterviewTest.Tests;

public class OrderCreationTests
{
    // Al crear un pedido debe persistirse (recibir un Id) y publicarse su evento en la cola.
    [Fact]
    public async Task Crear_un_pedido_deberia_guardarlo_y_publicar_el_evento()
    {
        using var db = new SqliteTestDb();

        int customerId;
        using (var seed = db.NewContext())
        {
            var customer = new Customer { Name = "Alice", Email = "alice@example.com" };
            seed.Customers.Add(customer);
            await seed.SaveChangesAsync();
            customerId = customer.Id;
        }

        using var ctx = db.NewContext();
        var publisher = new CapturingEventPublisher();
        var handler = new CreateOrderCommandHandler(new OrderRepository(ctx), publisher, TestMapper.Create());

        var dto = await handler.Handle(new CreateOrderCommand(customerId, 99.90m), CancellationToken.None);

        Assert.True(dto.Id > 0);
        Assert.Equal(customerId, dto.CustomerId);
        Assert.Single(publisher.Published.OfType<OrderCreatedEvent>());
    }
}

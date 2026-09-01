using InterviewTest.Application.Orders.Commands.UpdateOrderStatus;
using InterviewTest.Domain.Entities;
using InterviewTest.Domain.Enums;
using InterviewTest.Infrastructure.Persistence.Repositories;
using InterviewTest.Tests.Support;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace InterviewTest.Tests;

public class OrderStatusUpdateTests
{
    // Al cambiar el estado de un pedido, el cambio debe quedar guardado.
    // La comprobación se hace desde un contexto NUEVO para verificar la persistencia real
    // (no basta con que la entidad haya cambiado en memoria durante la operación).
    [Fact]
    public async Task Cambiar_el_estado_deberia_persistirse()
    {
        using var db = new SqliteTestDb();

        int orderId;
        using (var seed = db.NewContext())
        {
            var customer = new Customer { Name = "Alice", Email = "alice@example.com" };
            var order = new Order { Customer = customer, TotalAmount = 10m, Status = OrderStatus.Pending };
            seed.Customers.Add(customer);
            seed.Orders.Add(order);
            await seed.SaveChangesAsync();
            orderId = order.Id;
        }

        using (var actContext = db.NewContext())
        {
            var handler = new UpdateOrderStatusCommandHandler(new OrderRepository(actContext));
            var updated = await handler.Handle(
                new UpdateOrderStatusCommand(orderId, OrderStatus.Shipped), CancellationToken.None);

            Assert.True(updated);
        }

        using var assertContext = db.NewContext();
        var reloaded = await assertContext.Orders.SingleAsync(o => o.Id == orderId);

        Assert.Equal(OrderStatus.Shipped, reloaded.Status);
    }
}

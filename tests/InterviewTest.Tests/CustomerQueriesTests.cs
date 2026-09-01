using InterviewTest.Application.Customers.Queries.GetAllCustomers;
using InterviewTest.Domain.Entities;
using InterviewTest.Domain.Enums;
using InterviewTest.Infrastructure.Persistence.Repositories;
using InterviewTest.Tests.Support;
using Xunit;

namespace InterviewTest.Tests;

public class CustomerQueriesTests
{
    // El listado de clientes debe reflejar cuántos pedidos tiene cada uno.
    [Fact]
    public async Task Listar_clientes_deberia_devolver_el_numero_de_pedidos()
    {
        using var db = new SqliteTestDb();

        using (var seed = db.NewContext())
        {
            var alice = new Customer { Name = "Alice", Email = "alice@example.com" };
            alice.Orders.Add(new Order { TotalAmount = 1m, Status = OrderStatus.Pending });
            alice.Orders.Add(new Order { TotalAmount = 2m, Status = OrderStatus.Confirmed });
            seed.Customers.Add(alice);
            await seed.SaveChangesAsync();
        }

        using var ctx = db.NewContext();
        var handler = new GetAllCustomersQueryHandler(new CustomerRepository(ctx), TestMapper.Create());

        var result = await handler.Handle(new GetAllCustomersQuery(), CancellationToken.None);

        Assert.Equal(2, Assert.Single(result).OrderCount);
    }
}

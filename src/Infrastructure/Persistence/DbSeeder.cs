using InterviewTest.Domain.Entities;
using InterviewTest.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace InterviewTest.Infrastructure.Persistence;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        await context.Database.EnsureCreatedAsync();

        if (await context.Customers.AnyAsync())
        {
            return;
        }

        var alice = new Customer { Name = "Alice García", Email = "alice@example.com" };
        var bob = new Customer { Name = "Bob Martín", Email = "bob@example.com" };

        context.Customers.AddRange(alice, bob);
        await context.SaveChangesAsync();

        context.Orders.AddRange(
            new Order { CustomerId = alice.Id, TotalAmount = 120.50m, Status = OrderStatus.Pending },
            new Order { CustomerId = alice.Id, TotalAmount = 89.99m, Status = OrderStatus.Confirmed },
            new Order { CustomerId = bob.Id, TotalAmount = 250.00m, Status = OrderStatus.Shipped });

        await context.SaveChangesAsync();
    }
}

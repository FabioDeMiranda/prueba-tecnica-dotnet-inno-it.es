using InterviewTest.Application.Orders.Dtos;
using InterviewTest.Domain.Entities;
using InterviewTest.Domain.Enums;
using InterviewTest.Tests.Support;
using Xunit;

namespace InterviewTest.Tests;

public class OrderMappingTests
{
    // El DTO de lectura de un pedido debe exponer el nombre del cliente asociado.
    [Fact]
    public void El_dto_de_pedido_deberia_incluir_el_nombre_del_cliente()
    {
        var mapper = TestMapper.Create();

        var order = new Order
        {
            Id = 1,
            CustomerId = 5,
            TotalAmount = 10m,
            Status = OrderStatus.Pending,
            Customer = new Customer { Id = 5, Name = "Alice García" }
        };

        var dto = mapper.Map<OrderDto>(order);

        Assert.Equal("Alice García", dto.CustomerName);
    }
}

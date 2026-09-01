using InterviewTest.Application.Orders.Commands.CreateOrder;
using InterviewTest.Application.Orders.Commands.UpdateOrderStatus;
using InterviewTest.Application.Orders.Queries.GetAllOrders;
using InterviewTest.Application.Orders.Queries.GetOrderById;
using InterviewTest.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InterviewTest.Api.Controllers;

[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly ISender _sender;

    public OrdersController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var orders = await _sender.Send(new GetAllOrdersQuery(), cancellationToken);
        return Ok(orders);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var order = await _sender.Send(new GetOrderByIdQuery(id), cancellationToken);
        return order is null ? NotFound() : Ok(order);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderRequest request, CancellationToken cancellationToken)
    {
        var dto = await _sender.Send(new CreateOrderCommand(request.CustomerId, request.TotalAmount), cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    [HttpPut("{id:int}/status")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateStatusRequest request, CancellationToken cancellationToken)
    {
        var updated = await _sender.Send(new UpdateOrderStatusCommand(id, request.NewStatus), cancellationToken);
        return updated ? NoContent() : NotFound();
    }
}

public record CreateOrderRequest(int CustomerId, decimal TotalAmount);

public record UpdateStatusRequest(OrderStatus NewStatus);

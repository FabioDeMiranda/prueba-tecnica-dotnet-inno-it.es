using InterviewTest.Application.Orders.Dtos;
using MediatR;

namespace InterviewTest.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(int CustomerId, decimal TotalAmount) : IRequest<OrderDto>;

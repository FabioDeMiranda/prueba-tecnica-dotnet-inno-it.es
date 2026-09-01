using InterviewTest.Application.Orders.Dtos;
using MediatR;

namespace InterviewTest.Application.Orders.Queries.GetOrderById;

public record GetOrderByIdQuery(int Id) : IRequest<OrderDto?>;

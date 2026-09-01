using InterviewTest.Application.Orders.Dtos;
using MediatR;

namespace InterviewTest.Application.Orders.Queries.GetAllOrders;

public record GetAllOrdersQuery : IRequest<IReadOnlyList<OrderDto>>;

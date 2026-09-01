using InterviewTest.Domain.Enums;
using MediatR;

namespace InterviewTest.Application.Orders.Commands.UpdateOrderStatus;

public record UpdateOrderStatusCommand(int OrderId, OrderStatus NewStatus) : IRequest<bool>;

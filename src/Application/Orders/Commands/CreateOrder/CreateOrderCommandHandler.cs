using AutoMapper;
using InterviewTest.Application.Common.Messaging;
using InterviewTest.Application.Orders.Dtos;
using InterviewTest.Domain.Entities;
using InterviewTest.Domain.Enums;
using InterviewTest.Domain.Events;
using InterviewTest.Domain.Repositories;
using Mapster;
using MediatR;

namespace InterviewTest.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderDto>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IEventPublisher _eventPublisher;
    private readonly IMapper _mapper;

    public CreateOrderCommandHandler(
        IOrderRepository orderRepository,
        IEventPublisher eventPublisher,
        IMapper mapper)
    {
        _orderRepository = orderRepository;
        _eventPublisher = eventPublisher;
        _mapper = mapper;
    }

    public async Task<OrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        // Mapster: Command -> Entity (mapeo por convención de nombres).
        var order = request.Adapt<Order>();
        order.Status = OrderStatus.Pending;
        order.CreatedAt = DateTime.UtcNow;

        await _orderRepository.AddAsync(order, cancellationToken);
        await _orderRepository.SaveChangesAsync(cancellationToken);

        // Notificamos a los servicios suscritos (facturación, almacén) publicando el evento.
        // TODO: migrar a un patrón outbox para garantizar la entrega si el proceso se cae.
        _eventPublisher
            .PublishAsync(new OrderCreatedEvent(order.Id, order.CustomerId, order.TotalAmount), cancellationToken)
            .GetAwaiter()
            .GetResult();

        return _mapper.Map<OrderDto>(order);
    }
}

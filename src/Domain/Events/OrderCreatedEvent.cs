namespace InterviewTest.Domain.Events;

/// <summary>
/// Evento de dominio que se publicaría en la cola (RabbitMQ) cuando se crea un pedido,
/// para que otros servicios (facturación, almacén) reaccionen de forma asíncrona.
/// </summary>
public record OrderCreatedEvent(int OrderId, int CustomerId, decimal TotalAmount);

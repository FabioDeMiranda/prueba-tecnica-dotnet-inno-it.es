namespace InterviewTest.Application.Common.Messaging;

/// <summary>
/// Abstracción del bus de mensajería. En producción la implementación publicaría
/// en RabbitMQ; aquí se usa una implementación de consola para poder arrancar sin infra.
/// </summary>
public interface IEventPublisher
{
    Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
        where TEvent : class;
}

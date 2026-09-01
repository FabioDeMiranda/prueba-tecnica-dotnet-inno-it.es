using InterviewTest.Application.Common.Messaging;
using Microsoft.Extensions.Logging;

namespace InterviewTest.Infrastructure.Messaging;

/// <summary>
/// Implementación de <see cref="IEventPublisher"/> que simula la publicación en la cola.
/// En producción, esta clase se sustituiría por una que publica en RabbitMQ.
/// </summary>
public class ConsoleEventPublisher : IEventPublisher
{
    private readonly ILogger<ConsoleEventPublisher> _logger;

    public ConsoleEventPublisher(ILogger<ConsoleEventPublisher> logger)
    {
        _logger = logger;
    }

    public Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
        where TEvent : class
    {
        _logger.LogInformation("[QUEUE] Evento publicado: {EventType} -> {@Event}",
            typeof(TEvent).Name, @event);

        return Task.CompletedTask;
    }
}

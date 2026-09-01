using InterviewTest.Application.Common.Messaging;

namespace InterviewTest.Tests.Support;

/// <summary>Publicador de eventos falso que captura lo publicado para poder verificarlo.</summary>
public sealed class CapturingEventPublisher : IEventPublisher
{
    public List<object> Published { get; } = new();

    public Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
        where TEvent : class
    {
        Published.Add(@event);
        return Task.CompletedTask;
    }
}

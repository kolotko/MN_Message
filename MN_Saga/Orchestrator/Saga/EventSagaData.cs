using MassTransit;

namespace Orchestrator.Saga;

public class EventSagaData : SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }
    public string CurrentState { get; set; }
    
    public string Email { get; set; } = string.Empty;
    public bool Event1Complete { get; set; }
    public bool Event2Complete { get; set; }
}
using MassTransit;
using Orchestrator.Events;

namespace Orchestrator.Saga;

public class EventSaga : MassTransitStateMachine<EventSagaData>
{
    public State Event1State { get; set; }
    public State Event2State { get; set; }

    public Event<Event1> Event1 { get; set; }
    public Event<Event2> Event2 { get; set; }

    public EventSaga()
    {
        InstanceState(x => x.CurrentState);
        
        Event(() => Event1, e => e.CorrelateById(m => m.Message.EventId));
        Event(() => Event2, e => e.CorrelateById(m => m.Message.EventId));
        
        Initially(
            When(Event1)
                .Then(context =>
                {
                    context.Saga.Email = context.Message.Email;
                    context.Saga.Event1Complete = true;
                })
                .TransitionTo(Event1State)
                .Publish(context => new Event2()
                {
                    Email = context.Message.Email,
                    EventId = context.Message.EventId,
                }));

                During(Event1State,
            When(Event2)
                .Then(context => context.Saga.Event2Complete = true)
                .TransitionTo(Event1State));
    }
}
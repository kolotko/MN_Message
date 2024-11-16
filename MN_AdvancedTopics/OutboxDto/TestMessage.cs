namespace OutboxDto;

public class TestMessage
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Content { get; set; }
}
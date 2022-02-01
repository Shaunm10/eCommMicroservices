

namespace EventBus.Messages.Events;

public class IntegrationBaseEvent
{
    public IntegrationBaseEvent()
    {
        this.Id = Guid.NewGuid();
        this.CreationDateUtc = DateTime.UtcNow;
    }

    public IntegrationBaseEvent(Guid id, DateTime createDateTimeUtc)
    {
        this.Id = id;
        this.CreationDateUtc = createDateTimeUtc;
    }

    public Guid Id { get; set; }

    /// <summary>
    /// The DateTime this message was dispatched in Coordinated Universal Time.
    /// </summary>
    public DateTime CreationDateUtc { get; set; }
}

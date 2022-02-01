namespace EventBus.Messages.Events;

public class IntegrationBaseEvent
{
    public IntegrationBaseEvent()
    {
        this.Id = Guid.NewGuid();
        this.CreationDateUtc = DateTime.UtcNow;
    }

    public IntegrationBaseEvent(Guid id, DateTime createDate)
    {
        this.Id = id;
        this.CreationDateUtc = createDate;
    }

    public Guid Id { get; set; }

    /// <summary>
    /// The DateTime this message was dispatched in Coordinated Universal Time.
    /// </summary>
    public DateTime CreationDateUtc { get; set; }
}
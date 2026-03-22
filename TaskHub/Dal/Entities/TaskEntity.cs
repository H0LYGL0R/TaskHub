namespace Dal.Entities;

public sealed class TaskEntity
{
    public Guid Id { get; set; }

    public string? Title { get; set; }

    public Guid CreatedByUserId { get; set; }

    public DateTimeOffset CreatedUtc { get; set; }

    public User? CreatedByUser { get; set; }
}
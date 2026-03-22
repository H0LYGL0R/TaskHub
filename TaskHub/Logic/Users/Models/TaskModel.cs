namespace Api.Tasks.Models;

public sealed class TaskModel
{

    public TaskModel(Guid id, string? title, Guid createdByUserId, DateTimeOffset createdUtc)
    {
        Id = id;
        Title = title;
        CreatedByUserId = createdByUserId;
        CreatedUtc = createdUtc;
    }
    public TaskModel(Dal.Entities.TaskEntity entity)
    {
        Id = entity.Id;
        Title = entity.Title;
        CreatedByUserId = entity.CreatedByUserId;
        CreatedUtc = entity.CreatedUtc;
    }
    
    public Guid Id { get; }

    public string? Title { get; }

    public Guid CreatedByUserId { get; }

    public DateTimeOffset CreatedUtc { get; }
}
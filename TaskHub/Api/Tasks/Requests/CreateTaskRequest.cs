namespace Api.Controllers.Tasks.Requests;

public sealed record CreateTaskRequest
{
    public required string Title { get; init; }
    
    public Guid CreatedByUserId { get; init; }
}
namespace Api.Tasks.Requests;

public sealed record SetTaskTitleRequest
{
    public string? Title { get; init; }
}
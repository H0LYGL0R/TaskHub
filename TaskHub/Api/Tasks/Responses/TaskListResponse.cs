namespace Api.Tasks.Responses;


public sealed record TaskListResponse(IReadOnlyCollection<TaskResponse> TaskList);
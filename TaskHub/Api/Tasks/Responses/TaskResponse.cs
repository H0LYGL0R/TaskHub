namespace Api.Tasks.Responses;


public sealed record TaskResponse(Guid Id, string? Title, Guid CreatedByUserId, DateTimeOffset CreatedUtc)
{
    
}
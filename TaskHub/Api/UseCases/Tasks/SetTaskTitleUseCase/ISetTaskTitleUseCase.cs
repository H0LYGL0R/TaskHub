namespace Api.UseCases.Tasks.SetTaskTitleUseCase;

public interface ISetTaskTitleUseCase
{
    Task<bool> ExecuteAsync(Guid taskId, string title, CancellationToken cancellationToken);
}
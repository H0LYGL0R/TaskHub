namespace Api.UseCases.Tasks.DeleteTaskUseCase;

public interface IDeleteTaskUseCase
{
    Task<bool> ExecuteAsync(Guid taskId, CancellationToken cancellationToken);
}
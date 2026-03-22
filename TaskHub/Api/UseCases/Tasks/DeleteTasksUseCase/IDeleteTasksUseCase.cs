namespace Api.UseCases.Tasks.DeleteTasksUseCase;

public interface IDeleteTasksUseCase
{
    Task ExecuteAsync(CancellationToken cancellationToken);
}
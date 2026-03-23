using Api.Tasks.Models;

namespace Api.UseCases.Tasks.GetTasksUseCase;

public interface IGetTasksUseCase
{
    Task<IReadOnlyCollection<TaskModel>> ExecuteAsync(CancellationToken cancellationToken);
}
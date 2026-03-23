using Api.Tasks.Models;

namespace Api.UseCases.Tasks.GetTaskUseCase;

public interface IGetTaskUseCase
{
    Task<TaskModel?> ExecuteAsync(Guid taskId, CancellationToken cancellationToken);
}
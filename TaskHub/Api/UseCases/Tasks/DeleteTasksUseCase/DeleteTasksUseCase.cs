using Dal.Repositories.Interfaces;

namespace Api.UseCases.Tasks.DeleteTasksUseCase;

internal sealed class DeleteTasksUseCase(ITaskRepository taskRepository) : IDeleteTasksUseCase
{
    private readonly ITaskRepository _taskRepository = taskRepository;

    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        await _taskRepository.DeleteAllTasksAsync(cancellationToken);
    }
}
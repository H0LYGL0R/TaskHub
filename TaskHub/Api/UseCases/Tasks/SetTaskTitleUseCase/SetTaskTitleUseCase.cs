using Dal.Repositories.Interfaces;

namespace Api.UseCases.Tasks.SetTaskTitleUseCase;

internal sealed class SetTaskTitleUseCase(ITaskRepository taskRepository) : ISetTaskTitleUseCase
{
    private readonly ITaskRepository _taskRepository = taskRepository;

    public async Task<bool> ExecuteAsync(Guid taskId, string title, CancellationToken cancellationToken)
    {
        return await _taskRepository.SetTaskTitleAsync(taskId, title, cancellationToken);
    }
}
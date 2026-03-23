using Api.Tasks.Models;
using Dal.Repositories.Interfaces;

namespace Api.UseCases.Tasks.GetTasksUseCase;

internal sealed class GetTasksUseCase(ITaskRepository taskRepository) : IGetTasksUseCase
{
    private readonly ITaskRepository _taskRepository = taskRepository;

    public async Task<IReadOnlyCollection<TaskModel>> ExecuteAsync(CancellationToken cancellationToken)
    {
        var tasks = await _taskRepository.GetAllTasksAsync(cancellationToken);
        return tasks
            .Select(x => new TaskModel(x.Id, x.Title, x.CreatedByUserId, x.CreatedUtc))
            .ToList()
            .AsReadOnly();
    }
}
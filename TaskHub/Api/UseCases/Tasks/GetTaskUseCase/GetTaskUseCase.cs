using Api.Tasks.Models;
using Dal.Entities;
using Dal.Repositories.Interfaces;

namespace Api.UseCases.Tasks.GetTaskUseCase;

internal sealed class GetTaskUseCase(ITaskRepository taskRepository) : IGetTaskUseCase
{
    private readonly ITaskRepository _taskRepository = taskRepository;

    public async Task<TaskModel?> ExecuteAsync(Guid taskId, CancellationToken cancellationToken)
    {
        TaskEntity? taskEntity = await _taskRepository.GetTaskByIdAsync(taskId, cancellationToken);
        if (taskEntity is null)
        {
            return null;
        }

        return new TaskModel(taskEntity.Id, taskEntity.Title, taskEntity.CreatedByUserId, taskEntity.CreatedUtc);
    }
}
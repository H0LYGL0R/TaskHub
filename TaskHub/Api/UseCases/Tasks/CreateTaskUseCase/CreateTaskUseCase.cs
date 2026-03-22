using Api.Tasks.Models;
using Dal.Entities;
using Dal.Repositories.Interfaces;

namespace Api.UseCases.Tasks.CreateTaskUseCase;

internal sealed class CreateTaskUseCase(ITaskRepository taskRepository) : ICreateTaskUseCase
{
    public async Task<TaskModel?> ExecuteAsync(string title, Guid createdByUserId, CancellationToken cancellationToken)
    {
        TaskEntity? taskEntity = await 
            taskRepository.CreateTaskAsync(title, createdByUserId, DateTimeOffset.UtcNow, cancellationToken);
        return taskEntity is null ? null 
            : new TaskModel(taskEntity.Id, taskEntity.Title, taskEntity.CreatedByUserId, taskEntity.CreatedUtc);
    }
}


using Api.Services.TaskService;
using Api.Tasks.Models;
using Dal.Repositories.Interfaces;

public sealed class TaskService(ITaskRepository taskRepository) : ITaskService
{
    public async Task<TaskModel?> CreateTaskAsync(string title, Guid createdByUserId, CancellationToken ct)
    {
        Dal.Entities.TaskEntity? createdEntity = await taskRepository.CreateTaskAsync(title, createdByUserId, DateTimeOffset.UtcNow, ct);

        return createdEntity is null ? null : new TaskModel(createdEntity);
    }

    public async Task DeleteAllTasksAsync(CancellationToken ct)
    {
        await taskRepository.DeleteAllTasksAsync(ct);
    }

    public async Task<bool> DeleteTaskAsync(Guid id, CancellationToken ct)
    {
        return await taskRepository.DeleteTaskAsync(id, ct);
    }

    public async Task<IReadOnlyCollection<TaskModel>> GetAllTasksAsync(CancellationToken ct)
    {
        IReadOnlyCollection<Dal.Entities.TaskEntity> entities = await taskRepository.GetAllTasksAsync(ct);
        return entities.Select(e => new TaskModel(e)).ToList();
    }

    public async Task<TaskModel?> GetTaskByIdAsync(Guid id, CancellationToken ct)
    {
        Dal.Entities.TaskEntity? entity = await taskRepository.GetTaskByIdAsync(id, ct);
        return entity is null ? null : new TaskModel(entity);
    }

    public async Task<bool> SetTaskTitleAsync(Guid id, string title, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            return false;
        }

        return await taskRepository.UpdateTaskTitleAsync(id, title, ct);
    }
}

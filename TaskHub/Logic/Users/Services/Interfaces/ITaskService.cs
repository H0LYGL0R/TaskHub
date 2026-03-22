using Api.Tasks.Models;

namespace Api.Services.TaskService;

/// <summary>
/// Сервис задач
/// </summary>
public interface ITaskService
{
    Task<TaskModel?> CreateTaskAsync(string title, Guid createdByUserId, CancellationToken cancellationToken);

    Task<IReadOnlyCollection<TaskModel>> GetAllTasksAsync(CancellationToken cancellationToken);

    Task<TaskModel?> GetTaskByIdAsync(Guid taskId, CancellationToken cancellationToken);

    Task<bool> SetTaskTitleAsync(Guid taskId, string title, CancellationToken cancellationToken);

    Task<bool> DeleteTaskAsync(Guid id, CancellationToken ct);

    Task DeleteAllTasksAsync(CancellationToken cancellationToken);
}
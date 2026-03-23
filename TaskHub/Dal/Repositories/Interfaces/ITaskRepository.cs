using Dal.Entities;

namespace Dal.Repositories.Interfaces;


public interface ITaskRepository
{
    Task<TaskEntity?> CreateTaskAsync(string title, Guid createdByUserId, DateTimeOffset createdUtc, CancellationToken cancellationToken);

    Task<IReadOnlyCollection<TaskEntity>> GetAllTasksAsync(CancellationToken cancellationToken);

    Task<TaskEntity?> GetTaskByIdAsync(Guid taskId, CancellationToken cancellationToken);

    Task<bool> SetTaskTitleAsync(Guid taskId, string title, CancellationToken cancellationToken);

    Task<bool> DeleteTaskAsync(Guid taskId, CancellationToken cancellationToken);

    Task DeleteAllTasksAsync(CancellationToken cancellationToken);

    Task<bool> UpdateTaskTitleAsync(Guid id, string title, CancellationToken ct);
}
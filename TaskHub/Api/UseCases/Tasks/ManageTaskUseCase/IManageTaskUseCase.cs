using Api.Tasks.Responses;

namespace Api.UseCases.Tasks;

public interface IManageTaskUseCase
{
    Task<TaskResponse?> CreateTaskAsync(string title, Guid createdByUserId, CancellationToken cancellationToken);

    Task<TaskListResponse> GetAllTasksAsync(CancellationToken cancellationToken);

    Task<TaskResponse?> GetTaskByIdAsync(Guid taskId, CancellationToken cancellationToken);

    Task<bool> SetTaskTitleAsync(Guid taskId, string title, CancellationToken cancellationToken);

    Task<bool> DeleteTaskByIdAsync(Guid taskId, CancellationToken cancellationToken);

    Task DeleteAllTasksAsync(CancellationToken cancellationToken);
}
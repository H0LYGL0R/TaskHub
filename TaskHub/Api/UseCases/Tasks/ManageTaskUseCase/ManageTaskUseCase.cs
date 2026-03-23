using Api.Services.TaskService;
using Api.Tasks.Models;
using Api.Tasks.Responses;

namespace Api.UseCases.Tasks.ManageTaskUseCase;


internal sealed class ManageTaskUseCase(ITaskService taskService) : IManageTaskUseCase
{
    private readonly ITaskService _taskService = taskService;

    public async Task<TaskResponse?> CreateTaskAsync(string title, Guid createdByUserId, CancellationToken cancellationToken)
    {
        TaskModel? taskModel = await _taskService.CreateTaskAsync(title, createdByUserId, cancellationToken);
        return taskModel is null ? null : new TaskResponse(taskModel.Id, taskModel.Title, taskModel.CreatedByUserId, taskModel.CreatedUtc);
    }

    public async Task<TaskListResponse> GetAllTasksAsync(CancellationToken cancellationToken)
    {
        IReadOnlyCollection<TaskModel> tasks = await _taskService.GetAllTasksAsync(cancellationToken);
        var response = tasks
            .Select(x => new TaskResponse(x.Id, x.Title, x.CreatedByUserId, x.CreatedUtc))
            .ToList()
            .AsReadOnly();

        return new TaskListResponse(response);
    }

    public async Task<TaskResponse?> GetTaskByIdAsync(Guid taskId, CancellationToken cancellationToken)
    {
        TaskModel? taskModel = await _taskService.GetTaskByIdAsync(taskId, cancellationToken);
        return taskModel is null ? null : new TaskResponse(taskModel.Id, taskModel.Title, taskModel.CreatedByUserId, taskModel.CreatedUtc);
    }

    public async Task<bool> SetTaskTitleAsync(Guid taskId, string title, CancellationToken cancellationToken)
    {
        return await _taskService.SetTaskTitleAsync(taskId, title, cancellationToken);
    }

    public async Task<bool> DeleteTaskByIdAsync(Guid taskId, CancellationToken cancellationToken)
    {
        return await _taskService.DeleteTaskAsync(taskId, cancellationToken);
    }

    public async Task DeleteAllTasksAsync(CancellationToken cancellationToken)
    {
        await _taskService.DeleteAllTasksAsync(cancellationToken);
    }
}

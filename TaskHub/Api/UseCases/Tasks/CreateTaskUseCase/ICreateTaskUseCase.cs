using Api.Tasks.Models;

namespace Api.UseCases.Tasks.CreateTaskUseCase;

public interface ICreateTaskUseCase
{
    Task<TaskModel?> ExecuteAsync(string title, Guid createdByUserId, CancellationToken cancellationToken);
}
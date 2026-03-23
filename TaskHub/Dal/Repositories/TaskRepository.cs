using Dal.Context;
using Dal.Entities;
using Dal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dal.Repositories;

public sealed class TaskRepository(TaskDbContext dbContext) : ITaskRepository
{
    private readonly TaskDbContext _dbContext = dbContext;

    public async Task<TaskEntity?> CreateTaskAsync(string title, Guid createdByUserId, DateTimeOffset createdUtc, CancellationToken cancellationToken)
    {
        bool userExists = await _dbContext.Set<User>().AnyAsync(x => x.Id == createdByUserId, cancellationToken);
        if (userExists == false)
        {
            return null;
        }

        TaskEntity? taskEntity = new TaskEntity
        {
            Id = Guid.NewGuid(),
            Title = title,
            CreatedByUserId = createdByUserId,
            CreatedUtc = createdUtc
        };

        _dbContext.Tasks.Add(taskEntity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return taskEntity;
    }

    public async Task<IReadOnlyCollection<TaskEntity>> GetAllTasksAsync(CancellationToken cancellationToken)
    {
        List<TaskEntity> tasks = await _dbContext.Tasks
            .AsNoTracking()
            .OrderBy(x => x.CreatedUtc)
            .ToListAsync(cancellationToken);

        return tasks.AsReadOnly();
    }

    public async Task<TaskEntity?> GetTaskByIdAsync(Guid taskId, CancellationToken cancellationToken)
    {
        return await _dbContext.Tasks
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == taskId, cancellationToken);
    }

    public async Task<bool> SetTaskTitleAsync(Guid taskId, string title, CancellationToken cancellationToken)
    {
        TaskEntity? taskEntity = await _dbContext.Tasks.FirstOrDefaultAsync(x => x.Id == taskId, cancellationToken);
        if (taskEntity is null)
        {
            return false;
        }

        taskEntity.Title = title;
        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<bool> DeleteTaskAsync(Guid taskId, CancellationToken cancellationToken)
    {
        TaskEntity? taskEntity = await _dbContext.Tasks.FirstOrDefaultAsync(x => x.Id == taskId, cancellationToken);
        if (taskEntity is null)
        {
            return false;
        }

        _dbContext.Tasks.Remove(taskEntity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task DeleteAllTasksAsync(CancellationToken cancellationToken)
    {
        List<TaskEntity> tasks = await _dbContext.Tasks.ToListAsync(cancellationToken);
        if (tasks.Count is 0)
        {
            return;
        }

        _dbContext.Tasks.RemoveRange(tasks);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
    
    public async Task<bool> UpdateTaskTitleAsync(Guid id, string title, CancellationToken ct)
    {
        TaskEntity? task = await _dbContext.Tasks.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (task is null)
        {
            return false;
        }

        task.Title = title;

        await _dbContext.SaveChangesAsync(ct);
        return true;
    }
}

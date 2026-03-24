using TaskManager.Application.Interfaces;
using TaskManager.Domain.Entities;

namespace TaskManager.Infrastructure.Repositories;

public class InMemoryTaskRepository : ITaskRepository
{
    private readonly List<TaskItem> _tasks = new();

    public Task AddAsync(TaskItem task)
    {
        _tasks.Add(task);
        return Task.CompletedTask;
    }

    public Task<List<TaskItem>> GetAllAsync()
    {
        return Task.FromResult(_tasks);
    }

    public Task<TaskItem?> GetByIdAsync(Guid id)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);
        return Task.FromResult(task);
    }

    public Task UpdateAsync(TaskItem task)
    {
        var existing = _tasks.FirstOrDefault(t => t.Id == task.Id);
        if (existing != null)
        {
            existing.Name = task.Name;
            existing.Description = task.Description;
            existing.Deadline = task.Deadline;
            existing.Status = task.Status;
        }

        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);
        if (task != null)
            _tasks.Remove(task);

        return Task.CompletedTask;
    }
}
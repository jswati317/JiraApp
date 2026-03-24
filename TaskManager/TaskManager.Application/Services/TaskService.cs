using TaskManager.Application.Interfaces;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Services;

public class TaskService
{
    private readonly ITaskRepository _repo;

    public TaskService(ITaskRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<TaskItem>> GetAllAsync()
        => await _repo.GetAllAsync();

    public async Task<TaskItem?> GetByIdAsync(Guid id)
        => await _repo.GetByIdAsync(id);

   public async Task<TaskItem> CreateAsync(TaskItem task)
{
    if (string.IsNullOrWhiteSpace(task.Name))
        throw new ArgumentException("Task name is required");

    if (task.Deadline <= DateTime.UtcNow)
        throw new ArgumentException("Deadline must be in the future");

    var validStatuses = new[] { "Todo", "InProgress", "Done" };
    if (!validStatuses.Contains(task.Status))
        throw new ArgumentException("Invalid status");

    await _repo.AddAsync(task);
    return task;
}

    public async Task UpdateAsync(TaskItem task)
        => await _repo.UpdateAsync(task);

    public async Task DeleteAsync(Guid id)
        => await _repo.DeleteAsync(id);
}
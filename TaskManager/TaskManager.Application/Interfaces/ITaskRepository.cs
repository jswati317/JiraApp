using TaskManager.Domain.Entities;

namespace TaskManager.Application.Interfaces;

public interface ITaskRepository
{
    Task<List<TaskItem>> GetAllAsync();
    Task<TaskItem?> GetByIdAsync(Guid id);
    Task AddAsync(TaskItem task);
    Task UpdateAsync(TaskItem task);
    Task DeleteAsync(Guid id);
}
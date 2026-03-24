namespace TaskManager.Domain.Entities;

public class TaskItem
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime Deadline { get; set; }

    public string Status { get; set; } = "Todo";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
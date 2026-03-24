using Azure.Data.Tables;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Entities;

namespace TaskManager.Infrastructure.Repositories;

public class AzureTaskRepository : ITaskRepository
{
    private readonly TableClient _tableClient;

    public AzureTaskRepository(string connectionString)
    {
        var serviceClient = new TableServiceClient(connectionString);
        _tableClient = serviceClient.GetTableClient("Tasks");
        _tableClient.CreateIfNotExists();
    }

    public async Task AddAsync(TaskItem task)
    {
        var entity = MapToEntity(task);
        await _tableClient.AddEntityAsync(entity);
    }

    public async Task<List<TaskItem>> GetAllAsync()
    {
        var results = new List<TaskItem>();

        await foreach (var entity in _tableClient.QueryAsync<TableEntity>())
        {
            results.Add(MapFromEntity(entity));
        }

        return results;
    }

    public async Task<TaskItem?> GetByIdAsync(Guid id)
    {
        try
        {
            var response = await _tableClient.GetEntityAsync<TableEntity>("Task", id.ToString());
            return MapFromEntity(response.Value);
        }
        catch
        {
            return null;
        }
    }

    public async Task UpdateAsync(TaskItem task)
    {
        var entity = MapToEntity(task);
        await _tableClient.UpsertEntityAsync(entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _tableClient.DeleteEntityAsync("Task", id.ToString());
    }

    private static TableEntity MapToEntity(TaskItem task)
    {
        return new TableEntity("Task", task.Id.ToString())
        {
            { "Name", task.Name },
            { "Description", task.Description },
            { "Deadline", task.Deadline },
            { "Status", task.Status },
            { "CreatedAt", task.CreatedAt }
        };
    }

    private static TaskItem MapFromEntity(TableEntity entity)
    {
        return new TaskItem
        {
            Id = Guid.Parse(entity.RowKey),
            Name = entity.GetString("Name"),
            Description = entity.GetString("Description"),
            Deadline = entity.GetDateTime("Deadline") ?? DateTime.UtcNow,
            Status = entity.GetString("Status"),
            CreatedAt = entity.GetDateTime("CreatedAt") ?? DateTime.UtcNow
        };
    }
}
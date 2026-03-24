using Moq;
using NUnit.Framework;
using TaskManager.Application.Interfaces;
using TaskManager.Application.Services;
using TaskManager.Domain.Entities;

namespace TaskManager.Tests;

public class TaskServiceTests
{
    private Mock<ITaskRepository> _repoMock;
    private TaskService _service;

    [SetUp]
    public void Setup()
    {
        _repoMock = new Mock<ITaskRepository>();
        _service = new TaskService(_repoMock.Object);
    }

    // ---------------------------
    // CREATE TASK - SUCCESS
    // ---------------------------
    [Test]
    public async Task CreateAsync_Should_Add_Task_When_Valid()
    {
        var task = new TaskItem
        {
            Name = "Valid Task",
            Description = "Test",
            Deadline = DateTime.UtcNow.AddDays(2),
            Status = "Todo"
        };

        await _service.CreateAsync(task);

        _repoMock.Verify(x => x.AddAsync(task), Times.Once);
    }

    // ---------------------------
    // CREATE TASK - EMPTY NAME
    // ---------------------------
    [Test]
    public void CreateAsync_Should_Throw_When_Name_Is_Empty()
    {
        var task = new TaskItem
        {
            Name = "",
            Deadline = DateTime.UtcNow.AddDays(1),
            Status = "Todo"
        };

        Assert.ThrowsAsync<ArgumentException>(() => _service.CreateAsync(task));
    }

    // ---------------------------
    // CREATE TASK - PAST DEADLINE
    // ---------------------------
    [Test]
    public void CreateAsync_Should_Throw_When_Deadline_In_Past()
    {
        var task = new TaskItem
        {
            Name = "Test",
            Deadline = DateTime.UtcNow.AddDays(-1),
            Status = "Todo"
        };

        Assert.ThrowsAsync<ArgumentException>(() => _service.CreateAsync(task));
    }

    // ---------------------------
    // CREATE TASK - INVALID STATUS
    // ---------------------------
    [Test]
    public void CreateAsync_Should_Throw_When_Status_Invalid()
    {
        var task = new TaskItem
        {
            Name = "Test",
            Deadline = DateTime.UtcNow.AddDays(1),
            Status = "RandomStatus"
        };

        Assert.ThrowsAsync<ArgumentException>(() => _service.CreateAsync(task));
    }

    // ---------------------------
    // CREATE TASK - LONG DESCRIPTION
    // ---------------------------
    [Test]
    public async Task CreateAsync_Should_Handle_Long_Description()
    {
        var task = new TaskItem
        {
            Name = "Test",
            Description = new string('a', 300),
            Deadline = DateTime.UtcNow.AddDays(1),
            Status = "Todo"
        };

        await _service.CreateAsync(task);

        _repoMock.Verify(x => x.AddAsync(task), Times.Once);
    }

    // ---------------------------
    // GET ALL TASKS
    // ---------------------------
    [Test]
    public async Task GetAllAsync_Should_Return_List()
    {
        var tasks = new List<TaskItem>
        {
            new TaskItem { Name = "Task1" }
        };

        _repoMock.Setup(x => x.GetAllAsync()).ReturnsAsync(tasks);

        var result = await _service.GetAllAsync();

        Assert.That(result, Is.Not.Null.And.Not.Empty);
    }

    // ---------------------------
    // GET TASK BY ID
    // ---------------------------
    [Test]
    public async Task GetByIdAsync_Should_Return_Task_When_Exists()
    {
        var id = Guid.NewGuid();
        var task = new TaskItem { Id = id, Name = "Task1" };

        _repoMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(task);

        var result = await _service.GetByIdAsync(id);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.EqualTo(id));
    }

    // ---------------------------
    // UPDATE TASK
    // ---------------------------
    [Test]
    public async Task UpdateAsync_Should_Call_Repository()
    {
        var task = new TaskItem
        {
            Id = Guid.NewGuid(),
            Name = "Updated Task"
        };

        await _service.UpdateAsync(task);

        _repoMock.Verify(x => x.UpdateAsync(task), Times.Once);
    }

    // ---------------------------
    // DELETE TASK
    // ---------------------------
    [Test]
    public async Task DeleteAsync_Should_Call_Repository()
    {
        var id = Guid.NewGuid();

        await _service.DeleteAsync(id);

        _repoMock.Verify(x => x.DeleteAsync(id), Times.Once);
    }
}
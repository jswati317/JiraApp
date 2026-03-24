using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Services;
using TaskManager.Domain.Entities;

namespace TaskManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly TaskService _service;

    public TasksController(TaskService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var task = await _service.GetByIdAsync(id);
        return task == null ? NotFound() : Ok(task);
    }

    [HttpPost]
    public async Task<IActionResult> Create(TaskItem task)
    {
        var result = await _service.CreateAsync(task);
        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }

    [HttpPut]
    public async Task<IActionResult> Update(TaskItem task)
    {
        await _service.UpdateAsync(task);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
﻿using Api.Attributes;
using Api.Controllers.Tasks.Requests;
using Api.Tasks.Requests;
using Api.Tasks.Responses;
using Api.UseCases.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Api.Tasks;

[ApiController]
[Route("tasks")]
[ResponseTimeHeader]
[StudentInfoHeaders]
public sealed class TasksController(IManageTaskUseCase taskUseCase) : ControllerBase
{
    private readonly IManageTaskUseCase _taskUseCase = taskUseCase;
    private const string GetTaskByIdRouteName = "GetTaskById";

    [HttpPost]
    public async Task<ActionResult<TaskResponse>> CreateTaskAsync(
        [FromBody] CreateTaskRequest? request,
        CancellationToken cancellationToken)
    {
        if (request is null)
        {
            return BadRequest("Тело запроса отсутствует");
        }

        if (string.IsNullOrWhiteSpace(request.Title))
        {
            return BadRequest("Название задачи не задано");
        }

        if (request.CreatedByUserId == Guid.Empty)
        {
            return BadRequest("Идентификатор пользователя-создателя не задан");
        }

        TaskResponse? taskResponse = await _taskUseCase.CreateTaskAsync(request.Title, request.CreatedByUserId, 
            cancellationToken);
        if (taskResponse is null)
        {
            return NotFound("Пользователь-создатель не найден");
        }

        return CreatedAtRoute(GetTaskByIdRouteName, new { id = taskResponse.Id }, taskResponse);
    }
    
    [HttpGet]
    public async Task<ActionResult<TaskListResponse>> GetAllTasksAsync(CancellationToken cancellationToken)
    {
        TaskListResponse response = await _taskUseCase.GetAllTasksAsync(cancellationToken);
        return Ok(response);
    }
    

    [HttpGet("{id}", Name = GetTaskByIdRouteName)]
    public async Task<ActionResult<TaskResponse>> GetTaskByIdAsync([FromRouteTaskId] Guid id, CancellationToken cancellationToken)
    {
        TaskResponse? taskResponse = await _taskUseCase.GetTaskByIdAsync(id, cancellationToken);
        if (taskResponse is null)
        {
            return NotFound();
        }

        return Ok(taskResponse);
    }
    
    [HttpPut("{id}/title")]
    public async Task<IActionResult> SetTaskTitleAsync(
        [FromRouteTaskId] Guid id,
        [FromBody] SetTaskTitleRequest? request,
        CancellationToken cancellationToken)
    {
        if (request is null)
        {
            return BadRequest("Тело запроса отсутствует");
        }

        if (string.IsNullOrWhiteSpace(request.Title))
        {
            return BadRequest("Название задачи не задано");
        }

        bool isUpdated = await _taskUseCase.SetTaskTitleAsync(id, request.Title, cancellationToken);
        if (!isUpdated)
        {
            return NotFound();
        }

        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTaskByIdAsync([FromRouteTaskId] Guid id, CancellationToken cancellationToken)
    {
        bool isDeleted = await _taskUseCase.DeleteTaskByIdAsync(id, cancellationToken);
        if (!isDeleted)
        {
            return NotFound();
        }

        return NoContent();
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteAllTasksAsync(CancellationToken cancellationToken)
    {
        await _taskUseCase.DeleteAllTasksAsync(cancellationToken);
        return NoContent();
    }
}

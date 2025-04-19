using MediatR;
using Microsoft.AspNetCore.Mvc;
using TodoAppVSA.Features.Tasks.Commands;
using TodoAppVSA.Features.Tasks.Queries;

namespace TodoAppVSA.Features.Tasks;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    private readonly IMediator _mediator;

    public TaskController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetallTasks()
    {
        var tasks = await _mediator.Send(new GetTasksQuery());
        return Ok(tasks);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTaskItem(CreateTaskCommand command)
    {
        return Ok(await _mediator.Send(command));
    }
}
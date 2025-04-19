using MediatR;

namespace TodoAppVSA.Features.Tasks.Queries;

public record GetTasksQuery : IRequest<List<TaskItem>>;
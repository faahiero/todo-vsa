using MediatR;

namespace TodoAppVSA.Features.Tasks.Commands;

public record CreateTaskCommand(string Title, string Description) : IRequest<Guid>;
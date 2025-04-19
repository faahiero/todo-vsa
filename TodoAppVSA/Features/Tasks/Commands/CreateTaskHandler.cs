using MediatR;
using TodoAppVSA.Infrastructure;

namespace TodoAppVSA.Features.Tasks.Commands;

public class CreateTaskHandler : IRequestHandler<CreateTaskCommand, Guid>
{
    private readonly AppDbContext _dbContext;

    public CreateTaskHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var taskItem = new TaskItem
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            CreatedAt = DateTime.UtcNow
        };
        
        _dbContext.Tasks.Add(taskItem);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return taskItem.Id;
    }
}
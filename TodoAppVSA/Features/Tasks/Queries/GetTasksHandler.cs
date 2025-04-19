using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoAppVSA.Infrastructure;

namespace TodoAppVSA.Features.Tasks.Queries;

public class GetTasksHandler : IRequestHandler<GetTasksQuery, List<TaskItem>>
{
    private readonly AppDbContext _dbContext;

    public GetTasksHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<TaskItem>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
    {
        var tasks = await _dbContext.Tasks.ToListAsync(cancellationToken);
        return tasks;
    }
}
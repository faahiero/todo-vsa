using Microsoft.EntityFrameworkCore;
using TodoAppVSA.Features.Tasks.Commands;
using TodoAppVSA.Infrastructure;
using Xunit;

namespace TodoAppVSA.Features.Tasks;

public class TaskTests
{
    [Fact]
    public async Task CreateTask_ShouldReturnValidGuid()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;
        
        await using var context = new AppDbContext(options);
        var handler = new CreateTaskHandler(context);
        var command = new CreateTaskCommand("Test Task", "Test Description");
        
        var result = await handler.Handle(command, CancellationToken.None);
        
        Assert.NotEqual(Guid.Empty, result);
    }
    
}
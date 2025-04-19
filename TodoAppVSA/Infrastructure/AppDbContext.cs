using Microsoft.EntityFrameworkCore;
using TodoAppVSA.Features;

namespace TodoAppVSA.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }
    
    public DbSet<TaskItem> Tasks => Set<TaskItem>();
}
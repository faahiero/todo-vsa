using System.ComponentModel.DataAnnotations;

namespace TodoAppVSA.Features;

public class TaskItem
{
    public Guid Id { get; set; }
    [MaxLength(100)]
    public string Title { get; set; } = String.Empty;
    [MaxLength(200)]
    public string Description { get; set; } = String.Empty;
    public DateTime CreatedAt { get; set; }
    public bool IsCompleted { get; set; }
}
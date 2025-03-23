namespace Jobfinder.Domain.Entities;

public class JobCategory
{
    public Guid Id { get; set; }
    public string Category { get; set; } = string.Empty;
}
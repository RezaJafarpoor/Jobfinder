using static System.String;

namespace Jobfinder.Domain.Entities;

public class EmployerProfile
{
    public Guid Id { get; set; }
    public string CompanyName { get; set; } = Empty;
    public string WebsiteAddress { get; set; } = Empty;
    public string City { get; set; } = Empty;
    public int SizeOfCompany { get; set; }
    public User User { get; set; } = new();
    public Guid UserId { get; set; }
    public string Description { get; set; } = Empty;
}
using Jobfinder.Domain.ValueObjects;

namespace Jobfinder.Domain.Entities;

public class Company
{
    public Guid Id{ get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public string WebsiteAddress { get; set; } = string.Empty;
    public Location Location { get; set; } = new();
    public int SizeOfCompany { get; set; }
    public string Description { get; set; } = string.Empty;
}
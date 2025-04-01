using Jobfinder.Domain.ValueObjects;
using System.Data;
using System.Drawing;

namespace Jobfinder.Domain.Entities;

public class Company
{
    public Guid Id{ get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public string WebsiteAddress { get; set; } = string.Empty;
    public Location Location { get; set; } = new();
    public int SizeOfCompany { get; set; }
    public string Description { get; set; } = string.Empty;
    public EmployerProfile Owner { get; set; } = new();
    public Guid OwnerId { get; set; }

    public Company() {}

    public Company(EmployerProfile owner)
    {
        Owner = owner;
        OwnerId = owner.Id;
    }


    public void UpdateCompany(string? websiteAddress, Location? location, int? sizeOfCompany, string? description)
    {
        WebsiteAddress = websiteAddress ?? WebsiteAddress;
        Location = location ?? Location;
        SizeOfCompany = sizeOfCompany ?? SizeOfCompany;
        Description = description ?? Description;
    }
    
}
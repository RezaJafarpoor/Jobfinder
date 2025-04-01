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

    public Company(Guid ownerId,string websiteAddress, Location location, int sizeOfCompany, string description)
    {
        OwnerId = ownerId;
        WebsiteAddress = websiteAddress;
        Location = location;
        SizeOfCompany = sizeOfCompany;
        Description = description;
    }


    public void UpdateCompany(string? websiteAddress, Location? location, int? sizeOfCompany, string? description)
    {
        WebsiteAddress = websiteAddress ?? WebsiteAddress;
        Location = location ?? Location;
        SizeOfCompany = sizeOfCompany ?? SizeOfCompany;
        Description = description ?? Description;
    }
    
}
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
    public Guid Owner { get; set; }

    public Company() {}

    public Company(string companyName, string websiteAddress, Location location, int sizeOfCompany, string description, Guid owner)
    {
        CompanyName = companyName;
        WebsiteAddress = websiteAddress;
        Location = location;
        SizeOfCompany = sizeOfCompany;
        Description = description;
        Owner = owner;
    }


    public void UpdateCompany(string? websiteAddress, Location? location, int? sizeOfCompany, string? description)
    {
        WebsiteAddress = websiteAddress ?? WebsiteAddress;
        Location = location ?? Location;
        SizeOfCompany = sizeOfCompany ?? SizeOfCompany;
        Description = description ?? Description;
    }
    
}
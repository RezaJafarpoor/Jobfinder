using Jobfinder.Domain.ValueObjects;
using System.Data;
using System.Drawing;
using System.Text.Json.Serialization;

namespace Jobfinder.Domain.Entities;

public class Company
{
    public Guid Id{ get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public string WebsiteAddress { get; set; } = string.Empty;
    public Location Location { get; set; } = new();
    public string PictureUrl { get; set; } = string.Empty;
    public int SizeOfCompany { get; set; }
    public string Description { get; set; } = string.Empty;
    [JsonIgnore] public EmployerProfile Owner { get; set; } = null!;
    public Guid OwnerId { get; set; }

    public Company() {}
    public Company(EmployerProfile profile,string companyName,string websiteAddress, Location location, int sizeOfCompany, string description)
    {
        Owner = profile;
        CompanyName = companyName;
        OwnerId = profile.Id;
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
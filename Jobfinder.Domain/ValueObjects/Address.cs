using static System.String;

namespace Jobfinder.Domain.ValueObjects;

public class Address
{
    public string  City { get; set; } = Empty;
    public string Region { get; set; } = Empty;
}
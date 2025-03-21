using static System.String;

namespace Jobfinder.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public  string Username { get; set; } = Empty;
    public string  Password { get; set; } = Empty;
    public string Email { get; set; } = Empty;
}
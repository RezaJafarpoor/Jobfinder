
namespace Jobfinder.Domain.Entities;

public  class User
{
    public Guid Id { get; set; }
    public string Email { set; get; } = string.Empty;
    public string  Password { get; set; } = string.Empty;
}
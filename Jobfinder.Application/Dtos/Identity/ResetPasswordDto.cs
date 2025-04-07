namespace Jobfinder.Application.Dtos.Identity;

public record ResetPasswordDto(string Email, string Token , string NewPassword);
using Jobfinder.Application.Dtos.Profiles;

namespace Jobfinder.Application.Dtos.Identity;

public record IdentityResponse(string AccessToken, string RefreshToken, IProfile Profile);

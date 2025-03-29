using System.Text.Json.Serialization;

namespace Jobfinder.Application.Dtos.Identity;

public record RegisterDto
    (
        string Email,
        string Password,
        [property:JsonConverter(typeof(JsonStringEnumConverter))]UserType UserType);
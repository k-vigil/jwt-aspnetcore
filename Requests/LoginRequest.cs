namespace JwtExample.Requests;

public record LoginRequest
{
    public string Email { get; set; } = string.Empty;
}

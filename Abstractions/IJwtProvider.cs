namespace JwtExample.Abstractions;

public interface IJwtProvider
{
    string Generate(string email);
}

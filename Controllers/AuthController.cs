using JwtExample.Abstractions;
using JwtExample.Requests;
using Microsoft.AspNetCore.Mvc;

namespace JwtExample.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IJwtProvider _jwtProvider;

    public AuthController(IJwtProvider jwtProvider)
    {
        _jwtProvider = jwtProvider;
    }

    [HttpPost]
    public IActionResult Login(LoginRequest loginRequest)
    {
        if (string.IsNullOrEmpty(loginRequest.Email))
            return BadRequest();

        var token = _jwtProvider.Generate(loginRequest.Email);

        return Ok(new { jwt = token });
    }
}

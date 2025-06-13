using Microsoft.AspNetCore.Mvc;
using Eventourismo.Application.DTOs;

namespace Eventourismo.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    [HttpPost("login")]
    public async Task<ActionResult<ApiResponse<string>>> Login([FromBody] LoginRequest request)
    {
        try
        {
            // JWT authentication implementation would go here
            var token = "sample_jwt_token";
            return Ok(ApiResponse<string>.SuccessResult(token, "Login successful"));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse<string>.FailureResult(ex.Message));
        }
    }

    [HttpPost("register")]
    public async Task<ActionResult<ApiResponse<UserDto>>> Register([FromBody] RegisterRequest request)
    {
        try
        {
            // User registration implementation would go here
            var user = new UserDto { UserName = request.UserName, Email = request.Email };
            return Ok(ApiResponse<UserDto>.SuccessResult(user, "Registration successful"));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse<UserDto>.FailureResult(ex.Message));
        }
    }
}

public class LoginRequest
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class RegisterRequest
{
    public string Email { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
using FinancialHand.DTOs.Auth;
using FinancialHand.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinancialHand.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase 
{
  private AuthService _authService;

  public AuthController(AuthService authService) => _authService = authService;

  [HttpPost("cadastrar")]
  public async Task<IActionResult> Cadastrar(CreateUserDTO dto)
  {
    var result = await _authService.CreateUser(dto);

    if (result.IsFailed)
      return StatusCode(500);

    return Ok(result.Successes);
  }
}
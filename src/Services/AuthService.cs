using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using FinancialHand.Data;
using FinancialHand.DTOs.Auth;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace FinancialHand.Services;

public class AuthService
{
  private IMapper _mapper;
  private UserManager<IdentityUser<int>> _userManager;

  public AuthService(IMapper mapper, UserManager<IdentityUser<int>> userManager)
  {
    _mapper = mapper;
    _userManager = userManager;
  }

  public async Task<Result> CreateUser(CreateUserDTO dto)
  {
    var userIdentity = _mapper.Map<IdentityUser<int>>(dto);
    var resultIdentity = await _userManager.CreateAsync(userIdentity, dto.Password);

    if (resultIdentity.Succeeded)
      return Result.Ok().WithSuccess(CreateToken(userIdentity));

    return Result.Fail("Fail Sign In");
  }

  public string CreateToken(IdentityUser<int> user)
  {
    Claim[] direitosUsuario = new Claim[]
    {
      new Claim("username", user.UserName),
      new Claim("id", user.Id.ToString())
    };

    var chave = new SymmetricSecurityKey(
      Encoding.UTF8.GetBytes("sgduihgiugarh89724grkkhnlg3qhug4238902gt34")
    );
    var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);
    var token = new JwtSecurityToken(
      claims: direitosUsuario,
      signingCredentials: credenciais,
      expires: DateTime.UtcNow.AddHours(1)
    );

    var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
    
    return tokenString;
  }
}
using AutoMapper;
using FinancialHand.DTOs.Auth;
using FinancialHand.DTOs.Despesa;
using FinancialHand.DTOs.Receita;
using FinancialHand.Models;
using Microsoft.AspNetCore.Identity;

namespace FinancialHand.Profiles;

public class UserProfile : Profile
{
  public UserProfile()
  {
      CreateMap<CreateUserDTO, IdentityUser<int>>().ReverseMap();
  }
}
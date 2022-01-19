using AutoMapper;
using FinancialHand.DTOs.Receita;
using FinancialHand.Models;

namespace FinancialHand.Profiles;

public class CashFlowProfile : Profile
{
  public CashFlowProfile()
  {
      CreateMap<CreateReceitaDTO, CashFlow>();
      CreateMap<CashFlow, ReadReceitaDTO>();
  }
}
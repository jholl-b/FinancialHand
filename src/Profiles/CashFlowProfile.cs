using AutoMapper;
using FinancialHand.DTOs.Despesa;
using FinancialHand.DTOs.Receita;
using FinancialHand.Models;

namespace FinancialHand.Profiles;

public class CashFlowProfile : Profile
{
  public CashFlowProfile()
  {
      CreateMap<CreateReceitaDTO, CashFlow>();
      CreateMap<CashFlow, ReadReceitaDTO>();

      CreateMap<CreateDespesaDTO, CashFlow>();
      CreateMap<CashFlow, ReadDespesaDTO>();
  }
}
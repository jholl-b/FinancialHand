using AutoMapper;
using FinancialHand.Data;
using FinancialHand.DTOs.Resumo;
using FinancialHand.Models;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace FinancialHand.Services;

public class ResumoService {
  private IMapper _mapper;
  private FinancialContext _context;

  public ResumoService(IMapper mapper, FinancialContext context)
  {
    _mapper = mapper;
    _context = context;
  }

  public async Task<Result<ReadResumoDTO>> ReadMonthResume(int year, int month)
  {
    var flow = await _context.CashFlows
      .Where(x => x.Date.Year == year && x.Date.Month == month)
      .ToListAsync();

    if (flow is null || flow.Count() == 0)
      return Result.Fail("Regisro não encontrado.");

    var totalIncome = flow
      .Where(x => x.Type == FlowType.Incoming)
      .Sum(x => x.Value);
    var TotalOutcome = flow
      .Where(x => x.Type == FlowType.Outcoming)
      .Sum(x => x.Value);

    List<ReadCategoriaDTO> categories = new()
    {
      new()
      {
        Category = "Outras",
        Total = Math.Round(
          flow.Where(x => x.Type == FlowType.Outcoming && x.Category == Category.Others).Sum(x => x.Value), 2)
      },
      new()
      {
        Category = "Alimentação",
        Total = Math.Round(
          flow.Where(x => x.Type == FlowType.Outcoming && x.Category == Category.Food).Sum(x => x.Value), 2)
      },
      new()
      {
        Category = "Saúde",
        Total = Math.Round(
          flow.Where(x => x.Type == FlowType.Outcoming && x.Category == Category.Health).Sum(x => x.Value), 2)
      },
      new()
      {
        Category = "Moradia",
        Total = Math.Round(
          flow.Where(x => x.Type == FlowType.Outcoming && x.Category == Category.Home).Sum(x => x.Value), 2)
      },
      new()
      {
        Category = "Transporte",
        Total = Math.Round(
          flow.Where(x => x.Type == FlowType.Outcoming && x.Category == Category.Transport).Sum(x => x.Value), 2)
      },
      new()
      {
        Category = "Educação",
        Total = Math.Round(
          flow.Where(x => x.Type == FlowType.Outcoming && x.Category == Category.Education).Sum(x => x.Value), 2)
      },
      new()
      {
        Category = "Lazer",
        Total = Math.Round(
          flow.Where(x => x.Type == FlowType.Outcoming && x.Category == Category.Entertainment).Sum(x => x.Value), 2)
      },
      new()
      {
        Category = "Imprevistos",
        Total = Math.Round(
          flow.Where(x => x.Type == FlowType.Outcoming && x.Category == Category.Unforeseen).Sum(x => x.Value), 2)
      }
    };

    var resume = new ReadResumoDTO()
    {
      TotalIncome = Math.Round(totalIncome, 2),
      TotalOutcome = Math.Round(TotalOutcome, 2),
      Balance = Math.Round(totalIncome - TotalOutcome, 2),
      Categories = categories
    };

    return Result.Ok<ReadResumoDTO>(resume);
  }
}
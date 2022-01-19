using AutoMapper;
using FinancialHand.Data;
using FinancialHand.DTOs.Receita;
using FinancialHand.Models;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinancialHand.Services;

public class ReceitaService
{
  private IMapper _mapper;
  private FinancialContext _context;

  public ReceitaService(IMapper mapper, FinancialContext context)
  {
      _mapper = mapper;
      _context = context;
  }

  public async Task<Result<ReadReceitaDTO>> CreateCashFlowAsync(CreateReceitaDTO dto)
  {
    var previousFlow = await _context
      .CashFlows
      .Where(x => x.Value == dto.Value 
              && x.Date.Month == dto.Date!.Value.Month
              && x.Description.ToUpper() == dto.Description.ToUpper())
      .FirstOrDefaultAsync();

    if (dto.Value <= 0)
      return Result.Fail("Valor deve ser maior que 0.");

    if (previousFlow is not null)
      return Result.Fail("Receita já cadastrada para este mês.");

    var flow = _mapper.Map<CashFlow>(dto);
    flow.Type = FlowType.Incoming;

    _context.CashFlows.Add(flow);
    await _context.SaveChangesAsync();

    var readFlow = _mapper.Map<ReadReceitaDTO>(flow);

    return Result.Ok<ReadReceitaDTO>(readFlow);
  }

  public async Task<ActionResult<List<ReadReceitaDTO>>> ReadCashFlowAsync()
  {
    var flow = await _context.CashFlows.Where(x => x.Type == FlowType.Incoming).ToListAsync();
    return _mapper.Map<List<ReadReceitaDTO>>(flow);
  }
}
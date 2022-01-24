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
          && x.Type == FlowType.Incoming
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

  public async Task<List<ReadReceitaDTO>> ReadCashFlowAsync()
  {
    var flow = await _context.CashFlows
      .Where(x => x.Type == FlowType.Incoming)
      .ToListAsync();
    return _mapper.Map<List<ReadReceitaDTO>>(flow);
  }

  public async Task<Result<List<ReadReceitaDTO>>> ReadCashFlowAsync(string descricao)
  {
    var flow = await _context.CashFlows
      .Where(x => EF.Functions.Like(x.Description.ToUpper(), $"%{descricao.ToUpper()}%")
        && x.Type == FlowType.Incoming)
      .ToListAsync();

    if (flow is null)
      return Result.Fail("Receita não encontrada.");

    return Result.Ok<List<ReadReceitaDTO>>(_mapper.Map<List<ReadReceitaDTO>>(flow));
  }

  public async Task<Result<ReadReceitaDTO>> ReadSingleCashFlowAsync(int id)
  {
    var flow = await _context.CashFlows
      .Where(x => x.Type == FlowType.Incoming && x.Id == id)
      .FirstOrDefaultAsync();

    if (flow is null)
      return Result.Fail("Receita não encontrada.");

    return Result.Ok<ReadReceitaDTO>(_mapper.Map<ReadReceitaDTO>(flow));
  }

  public async Task<Result> UpdateFlowAsync(int id, CreateReceitaDTO dto)
  {
    var flow = await _context.CashFlows
      .FirstOrDefaultAsync(x => x.Id == id && x.Type == FlowType.Incoming);

    if (dto.Value <= 0)
      return Result.Fail("Valor deve ser maior que 0.");

    if (flow is null)
      return Result.Fail("Registro não encontrado.");

    if (FlowValidation(flow, dto))
      return Result.Fail("Receita já cadastrada para este mês.");
          
    _mapper.Map(dto, flow);
    await _context.SaveChangesAsync();

    return Result.Ok();
  }

  public async Task<Result> DeleteFlowAsync(int id)
  {
    var flow = await _context.CashFlows.FirstOrDefaultAsync(x => 
      x.Id == id);

    if (flow is null)
      return Result.Fail("Registro não encontrado.");

    _context.Remove(flow);
    await _context.SaveChangesAsync();

    return Result.Ok();
  }

  private bool FlowValidation (CashFlow flow, CreateReceitaDTO dto) =>
    flow.Value == dto.Value 
    && flow.Date.Month == dto.Date!.Value.Month
    && flow.Type == FlowType.Incoming
    && flow.Description.ToUpper() == dto.Description.ToUpper();
}
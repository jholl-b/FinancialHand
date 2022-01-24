using AutoMapper;
using FinancialHand.Data;
using FinancialHand.DTOs.Despesa;
using FinancialHand.Models;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace FinancialHand.Services;

public class DespesaService
{
  private IMapper _mapper;
  private FinancialContext _context;

  public DespesaService(IMapper mapper, FinancialContext context)
  {
    _mapper = mapper;
    _context = context;
  }

  public async Task<Result<ReadDespesaDTO>> CreateCashFlowAsync(CreateDespesaDTO dto)
  {
    var previousFlow = await _context
      .CashFlows
      .Where(x => x.Value == dto.Value 
          && x.Date.Month == dto.Date!.Value.Month
          && x.Type == FlowType.Outcoming
          && x.Description.ToUpper() == dto.Description.ToUpper())
      .FirstOrDefaultAsync();

    if (dto.Value <= 0)
      return Result.Fail("Valor deve ser maior que 0.");

    if (previousFlow is not null)
      return Result.Fail("Despesa já cadastrada para este mês.");

    var flow = _mapper.Map<CashFlow>(dto);
    flow.Type = FlowType.Outcoming;

    _context.CashFlows.Add(flow);
    await _context.SaveChangesAsync();

    var readFlow = _mapper.Map<ReadDespesaDTO>(flow);

    return Result.Ok<ReadDespesaDTO>(readFlow);
  }

  public async Task<List<ReadDespesaDTO>> ReadCashFlowAsync()
  {
    var flow = await _context.CashFlows.Where(x => 
      x.Type == FlowType.Outcoming).ToListAsync();
    return _mapper.Map<List<ReadDespesaDTO>>(flow);
  }

  public async Task<Result<List<ReadDespesaDTO>>> ReadCashFlowAsync(string descricao)
  {
    var flow = await _context.CashFlows
      .Where(x => EF.Functions.Like(x.Description.ToUpper(), $"%{descricao.ToUpper()}%")
        && x.Type == FlowType.Outcoming)
      .ToListAsync();

    if (flow is null)
      return Result.Fail("Despesa não encontrada.");

    return Result.Ok<List<ReadDespesaDTO>>(_mapper.Map<List<ReadDespesaDTO>>(flow));
  }

  public async Task<Result<ReadDespesaDTO>> ReadSingleCashFlowAsync(int id)
  {
    var flow = await _context.CashFlows
      .Where(x => x.Type == FlowType.Outcoming && x.Id == id)
      .FirstOrDefaultAsync();

    if (flow is null)
      return Result.Fail("Despesa não encontrada.");

    return Result.Ok<ReadDespesaDTO>(_mapper.Map<ReadDespesaDTO>(flow));
  }

  public async Task<Result> UpdateFlowAsync(int id, CreateDespesaDTO dto)
  {
    var flow = await _context.CashFlows
      .FirstOrDefaultAsync(x => x.Id == id && x.Type == FlowType.Outcoming);

    if (dto.Value <= 0)
      return Result.Fail("Valor deve ser maior que 0.");

    if (flow is null)
      return Result.Fail("Registro não encontrado.");

    if (FlowValidation(flow, dto))
      return Result.Fail("Despesa já cadastrada para este mês.");
          
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

  private bool FlowValidation (CashFlow flow, CreateDespesaDTO dto) =>
    flow.Value == dto.Value 
    && flow.Date.Month == dto.Date!.Value.Month
    && flow.Type == FlowType.Outcoming
    && flow.Description.ToUpper() == dto.Description.ToUpper();
}
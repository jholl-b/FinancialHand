using FinancialHand.Data;
using FinancialHand.DTOs.Receita;
using FinancialHand.Models;
using FinancialHand.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinancialHand.Controllers;

[ApiController]
[Route("[controller]")]
public class ReceitasController : ControllerBase
{
  private readonly ReceitaService _service;

  public ReceitasController(ReceitaService service) 
    => _service = service;

  [HttpPost]
  public async Task<ActionResult> PostAsync(CreateReceitaDTO dto)
  {
    var result = await _service.CreateCashFlowAsync(dto);

    if (result.IsFailed)
      return StatusCode(500, result.Errors.Select(x => x.Message).ToList());

    var flow = result.Value;
    
    return CreatedAtAction(nameof(Get), new {Id = flow.Id}, flow);
  }

  [HttpGet]
  public async Task<ActionResult<List<ReadReceitaDTO>>> Get() 
    => await _service.ReadCashFlowAsync();
}
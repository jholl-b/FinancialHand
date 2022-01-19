using FinancialHand.DTOs.Despesa;
using FinancialHand.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinancialHand.Controllers;

[ApiController]
[Route("[controller]")]
public class DespesasController : ControllerBase
{
  private readonly DespesaService _service;

  public DespesasController(DespesaService service) 
    => _service = service;

  [HttpPost]
  public async Task<ActionResult> PostAsync(CreateDespesaDTO dto)
  {
    var result = await _service.CreateCashFlowAsync(dto);

    if (result.IsFailed)
      return StatusCode(500, result.Errors.Select(x => x.Message).ToList());

    var flow = result.Value;
    
    return CreatedAtAction(nameof(GetWithId), new {Id = flow.Id}, flow);
  }

  [HttpPut("{id}")]
  public async Task<ActionResult> Put(int id, CreateDespesaDTO dto)
  {
    var result = await _service.UpdateFlowAsync(id, dto);

    if (result.IsFailed)
      return NotFound(result.Errors.Select(x => x.Message).ToList());

    return NoContent();
  }


  [HttpGet]
  public async Task<ActionResult<List<ReadDespesaDTO>>> Get() 
    => await _service.ReadCashFlowAsync();

  [HttpGet("{id}")]
  public async Task<ActionResult<ReadDespesaDTO>> GetWithId(int id)
  {
    var result = await _service.ReadSingleCashFlowAsync(id);

    if (result.IsFailed)
      return NotFound(result.Errors.Select( x => x.Message).ToList());

    return Ok(result.Value);
  }

  [HttpDelete("{id}")]
  public async Task<ActionResult> Delete(int id)
  {
    var result = await _service.DeleteFlowAsync(id);

    if (result.IsFailed)
      return NotFound(result.Errors.Select( x => x.Message).ToList());

    return NoContent();
  }
}
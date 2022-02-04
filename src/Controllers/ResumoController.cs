using FinancialHand.DTOs.Resumo;
using FinancialHand.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinancialHand.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class ResumoController: ControllerBase
{
  private ResumoService _service;

  public ResumoController(ResumoService service)
    => _service = service;

  [HttpGet("{ano}/{mes}")]
  public async Task<ActionResult<List<ReadResumoDTO>>> GetWithDate(int ano, int mes)
  {
     var result = await _service.ReadMonthResume(ano, mes);

    if (result.IsFailed)
      return NotFound(result.Errors.Select( x => x.Message).ToList());

    return Ok(result.Value);
  }
}
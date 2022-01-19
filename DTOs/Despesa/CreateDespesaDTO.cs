using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FinancialHand.DTOs.Despesa;

public class CreateDespesaDTO
{
  [JsonPropertyName("descricao")]
  [Required(ErrorMessage = "O campo descricao é obrigatório.")]
  public string Description { get; init; } = default!;
  [JsonPropertyName("valor")]
  [Required(ErrorMessage = "O campo valor é obrigatório.")]
  public decimal? Value { get; init; }
  [JsonPropertyName("data")]
  [Required(ErrorMessage = "O campo data é obrigatório.")]
  public DateTime? Date { get; init; }
}
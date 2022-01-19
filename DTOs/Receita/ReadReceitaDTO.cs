using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FinancialHand.DTOs.Receita;

public record ReadReceitaDTO
{
  [JsonPropertyName("id")]
  public int Id { get; init; }
  [JsonPropertyName("descricao")]
  public string Description { get; init; } = default!;
  [JsonPropertyName("valor")]
  // [DisplayFormat(DataFormatString="{0:0.00}")]
  public float Value { get; init; }
  [JsonPropertyName("data")]
  public DateTime Date { get; init; }
}
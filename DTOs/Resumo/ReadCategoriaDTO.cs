using System.Text.Json.Serialization;

namespace FinancialHand.DTOs.Resumo;

public record ReadCategoriaDTO
{
  [JsonPropertyName("categoria")]
  public string Category { get; init; } = default!;
  [JsonPropertyName("total")]
  public decimal Total { get; init; }
}
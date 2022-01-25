using System.Text.Json.Serialization;

namespace FinancialHand.DTOs.Resumo;

public record ReadResumoDTO
{
  [JsonPropertyName("receitasTotais")]
  public decimal TotalIncome { get; init; }
  [JsonPropertyName("despesasTotais")]
  public decimal TotalOutcome { get; set; }
  [JsonPropertyName("saldoFinal")]
  public decimal Balance { get; set; }
  [JsonPropertyName("categorias")]
  public List<ReadCategoriaDTO> Categories { get; set; } = default!;
}
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using FinancialHand.Models;

namespace FinancialHand.DTOs.Despesa;

public record ReadDespesaDTO
{
  [JsonPropertyName("id")]
  public int Id { get; init; }
  [JsonPropertyName("descricao")]
  public string Description { get; init; } = default!;
  [JsonPropertyName("valor")]
  public float Value { get; init; }
  [JsonPropertyName("data")]
  public DateTime Date { get; init; }
  [JsonPropertyName("categoria")]
  public string Category { get; set; } = default!;
}
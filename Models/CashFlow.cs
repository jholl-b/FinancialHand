namespace FinancialHand.Models;

public enum FlowType
{
  Incoming,
  Outcoming
} 

public record CashFlow 
{
  public int Id { get; set; }
  public string Description { get; init; } = default!;
  public decimal Value { get; init; }
  public DateTime Date { get; init; }
  public FlowType Type { get; set; }
}
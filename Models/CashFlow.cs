namespace FinancialHand.Models;

public enum FlowType
{
  Incoming,
  Outcoming
} 

public enum Category {
  Food,
  Health,
  Home,
  Transport,
  Education,
  Entertainment, //Leisure, //recriation // hobby
  Unforeseen,
  Others
}

public record CashFlow 
{
  public int Id { get; init; }
  public string Description { get; init; } = default!;
  public decimal Value { get; init; }
  public DateTime Date { get; init; }
  public FlowType Type { get; set; }
  public Category Category { get; set; } = Category.Others;
}
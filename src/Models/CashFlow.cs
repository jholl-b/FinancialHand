using System.Runtime.Serialization;

namespace FinancialHand.Models;

public enum FlowType
{
  Incoming,
  Outcoming
} 

public enum Category {
  [EnumMember(Value = "Outras")]
  Others,
  [EnumMember(Value = "Alimentação")]
  Food,
  [EnumMember(Value = "Saúde")]
  Health,
  [EnumMember(Value = "Moradia")]
  Home,
  [EnumMember(Value = "Transporte")]
  Transport,
  [EnumMember(Value = "Educação")]
  Education,
  [EnumMember(Value = "Lazer")]
  Entertainment,
  [EnumMember(Value = "Imprevistos")]
  Unforeseen
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
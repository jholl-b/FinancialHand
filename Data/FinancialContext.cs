using FinancialHand.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancialHand.Data;

public class FinancialContext : DbContext
{
  public DbSet<CashFlow> CashFlows { get; set; } = default!;

  public FinancialContext(DbContextOptions options) : base(options) {}
}
using FinancialHand.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinancialHand.Data;

public class FinancialContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>
{
  public DbSet<CashFlow> CashFlows { get; set; } = default!;

  public FinancialContext(DbContextOptions options) : base(options) {}
}
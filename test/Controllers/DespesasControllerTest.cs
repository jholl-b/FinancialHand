using System;
using AutoMapper;
using FinancialHand.Controllers;
using FinancialHand.Data;
using FinancialHand.Models;
using FinancialHand.Profiles;
using FinancialHand.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace FinancialHand.Test.Controllers;

public class DespesasControllerTest
{
  [Fact]
  public async void GetReturnAllDespesas()
  {
    //Arrange
    var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new CashFlowProfile()));
    var mapper = mapperConfig.CreateMapper();

    var dbOptions = new DbContextOptionsBuilder<FinancialContext>()
      .UseInMemoryDatabase(databaseName: "FinancialHand")
      .Options;

    using (var context = new FinancialContext(dbOptions))
    {
      context.CashFlows.Add(new CashFlow()
      {
        Description = "Despesa 01",
        Value = 22.30m,
        Date = new DateTime(2022, 01, 01, 10, 30, 10),
        Type = FlowType.Outcoming,
        Category = Category.Food
      });

      context.CashFlows.Add(new CashFlow()
      {
        Description = "Despesa 02",
        Value = 55.30m,
        Date = new DateTime(2022, 01, 01, 12, 30, 10),
        Type = FlowType.Outcoming
      });

      context.SaveChanges();
    }

    using(var context = new FinancialContext(dbOptions))
    {
      var service = new DespesaService(mapper, context);

      //Act
      var controller = new DespesasController(service);
      var result = await controller.Get(null);

      //Assert
      Console.WriteLine(result);
      Assert.Equal(result.Value!.Count, 2);
    }
  }
}
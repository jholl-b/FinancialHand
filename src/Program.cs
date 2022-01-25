using FinancialHand.Data;
using FinancialHand.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ReceitaService>();
builder.Services.AddScoped<DespesaService>();
builder.Services.AddScoped<ResumoService>();

builder.Services.AddControllers();

builder.Services.AddDbContext<FinancialContext>(opts =>
    opts.UseMySql(
        builder.Configuration.GetConnectionString("FinantialConnection"),
        MySqlServerVersion.AutoDetect(builder.Configuration.GetConnectionString("FinantialConnection"))
    )
    .LogTo(Console.WriteLine, LogLevel.Information)
    .EnableSensitiveDataLogging()
    .EnableDetailedErrors()
);

builder.Services.AddAutoMapper(
    AppDomain.CurrentDomain.GetAssemblies()
);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
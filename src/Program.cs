using System.Text;
using FinancialHand.Data;
using FinancialHand.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<AuthService>();
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

builder.Services.AddIdentity<IdentityUser<int>, IdentityRole<int>>()
    .AddEntityFrameworkStores<FinancialContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(opts => {
    opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opts => {
    opts.SaveToken = true;
    opts.TokenValidationParameters = new() {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                builder.Configuration["JWT:Token"]
            )
        ),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
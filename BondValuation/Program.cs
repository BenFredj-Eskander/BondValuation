using BondValuation.Infrastructure;
using BondValuation.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register services
builder.Services.AddScoped<IBondCsvParser, BondCsvParser>();
builder.Services.AddScoped<IBondCsvWriter, BondCsvWriter>();
builder.Services.AddScoped<IBondValuationEngine, BondValuationEngine>();
builder.Services.AddScoped<IBondValuationService, CouponBondValuationService>();
builder.Services.AddScoped<IBondValuationService, ZeroCouponBondValuationService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();
app.MapControllers();

app.Run();
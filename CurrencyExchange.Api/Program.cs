using CurrencyExchange.Api.Middlewares;
using CurrencyExchange.Domain.Messages;
using CurrencyExchange.Infrastructure;
using Domain.Entities;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var assembly = AppDomain.CurrentDomain.Load("CurrencyExchange.Application");
builder.Services.AddMediatR(assembly);

builder.Services.RegisterInfrastructure();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/currencyExchange/currency/defaultExchange", (IMediator mediator) =>
{

    return mediator.Send(new GetHomeCurrenciesRequest());
})
.WithName("GetHomeCurrencies");

app.MapGet("/currencyExchange/currency", () =>
{
    return new List<Currency>
    {
        new()
        {
            Id = 1,
            Code = "BRL",
            Name = "Brazilian real"
        },
        new()
        {
            Id = 2,
            Code = "USD",
            Name = "United States dollar"
        }
    };
})
.WithName("GetCurrencies");

app.Run();
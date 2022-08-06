using Domain.Entities;
using Domain.Messages;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/currencyExchange/currency/defaultExchange", () =>
{
    return new GetHomeCurrenciesResponse
    {
        FromCurrency = new()
        {
            Id = 1,
            Value = 1
        },
        ToCurrency = new()
        {
            Id = 2,
            Value = 1.02m
        },

    };
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
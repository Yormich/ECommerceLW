using ECommerceLW.Api.Search.Interfaces;
using ECommerceLW.Api.Search.Services;
using Polly;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IOrdersService, OrdersService>();
builder.Services.AddScoped<ICustomersService, CustomersService>();
builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<ISearchService, SearchService>();

builder.Services.AddHttpClient("OrdersService", (IServiceProvider provider, HttpClient config) =>
{
    IConfiguration configuration = provider.GetService<IConfiguration>()!;
    config.BaseAddress = new Uri(configuration["Services:Orders"]!);
});

builder.Services.AddHttpClient("ProductsService", (IServiceProvider provider, HttpClient config) =>
{
    IConfiguration configuration = provider.GetService<IConfiguration>()!;
    config.BaseAddress = new Uri(configuration["Services:Products"]!);
}).AddTransientHttpErrorPolicy(p =>
    p.WaitAndRetryAsync(5, _ => TimeSpan.FromMilliseconds(500))); // Exponential backoff


builder.Services.AddHttpClient("CustomersService", (IServiceProvider provider, HttpClient config) =>
{
    IConfiguration configuration = provider.GetService<IConfiguration>()!;
    config.BaseAddress = new Uri(configuration["Services:Customers"]!);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

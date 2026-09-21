using BillSale.API.Automapper;
using BillSale.API.Implementations;
using BillSale.BLL.Services;
using BillSale.BLL.Services.Automapper;
using BillSale.BLL.Services.Contracts;
using BillSale.Common;
using BillSale.DAL.Context;
using BillSale.DAL.Contracts.Repositories;
using BillSale.DAL.Repositories;
using BillSale.DAL.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
builder.Services.AddSingleton<IIdentityProvider, IdentityProvider>();

builder.Services.AddScoped<ITransferCertificateRepository, TransferCertificateRepository>();
builder.Services.AddScoped<ICertificateProductItemRepository, TransferCertificateProductRepository>();

builder.Services.AddScoped<ITransferCertificateService, TransferCertificateService>();
builder.Services.AddScoped<IDbWriterContext, DbWriterContext>();
builder.Services.AddAutoMapper(x =>
{
    x.AddProfile<ApiProfile>();
    x.AddProfile<ServiceProfile>();
});

//TODO: Вынести в Appsettings
builder.Services.AddDbContext<BillSaleContext>(opts =>
    opts.UseNpgsql("Host=localhost;Port=5432;Database=BillSale;Username=postgres;Password=12345"));
builder.Services.AddScoped<IUnitOfWork>(x => x.GetRequiredService<BillSaleContext>());
builder.Services.AddScoped<IReader>(x => x.GetRequiredService<BillSaleContext>());
builder.Services.AddScoped<IWriter>(x => x.GetRequiredService<BillSaleContext>());

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddHealthChecks();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi("swagger/v1/swagger.json");
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapHealthChecks("health");
app.MapControllers();

app.Run();

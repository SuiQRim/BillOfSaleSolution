using BillSale.API.Automapper;
using BillSale.API.Implementations;
using BillSale.API.Infrastructure;
using BillSale.BLL.Services;
using BillSale.BLL.Services.Automapper;
using BillSale.BLL.Services.Contracts;
using BillSale.BLL.Services.Validators.Certificate;
using BillSale.Common;
using BillSale.DAL.Context;
using BillSale.DAL.Contracts.Repositories;
using BillSale.DAL.Repositories;
using BillSale.DAL.Repositories.Contracts;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
builder.Services.AddSingleton<IIdentityProvider, IdentityProvider>();

builder.Services.AddScoped<ICertificateRepository, CertificateRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<ICertificateProductItemRepository, TransferCertificateProductRepository>();

builder.Services.AddScoped<ICertificateService, CertificateService>();

builder.Services.AddScoped<IValidateService, ValidateService>();
builder.Services.RegisterImplementationsOf<IValidator>(
    typeof(CertificateCreateModelValidator).Assembly,
    ServiceLifetime.Scoped);

builder.Services.AddScoped<IDbWriterContext, DbWriterContext>();

builder.Services.AddAutoMapper(x =>
{
    x.AddProfile<ApiProfile>();
    x.AddProfile<ServiceProfile>();
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<BillSaleContext>(opts =>
    opts.UseNpgsql(connectionString));
builder.Services.AddScoped<IUnitOfWork>(x => x.GetRequiredService<BillSaleContext>());
builder.Services.AddScoped<IReader>(x => x.GetRequiredService<BillSaleContext>());
builder.Services.AddScoped<IWriter>(x => x.GetRequiredService<BillSaleContext>());

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddControllers(opts => opts.Filters.Add<ExceptionFilter>());

builder.Services.AddHealthChecks();
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi("swagger/v1/swagger.json");
    app.UseSwaggerUI();
}

app.MapHealthChecks("health");

app.MapControllers();

app.Run();

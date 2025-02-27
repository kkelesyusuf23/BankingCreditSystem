using BankingCreditSystem.Application;
using BankingCreditSystem.Persistence;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// AutoMapper Configuration
builder.Services.AddAutoMapper(typeof(BankingCreditSystem.Application.Features.IndividualCustomers.Profiles.MappingProfiles).Assembly);

// Add Application & Persistence Services
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCustomExceptionMiddleware();

app.UseAuthorization();

app.MapControllers();

app.Run();

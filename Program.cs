using Microsoft.EntityFrameworkCore;
using PSP;
using PSP.Models.DTOs;
using PSP.Models.Entities;
using PSP.Repositories;
using PSP.Services;
using PSP.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<PSPDatabaseContext>(opt =>
    opt.UseInMemoryDatabase("PSP"));

builder.Services.AddScoped<IBaseRepository<Service>, ServicesRepository>();
builder.Services.AddScoped<IBaseRepository<ServiceSlot>, ServiceSlotsRepository>();
builder.Services.AddScoped<IBaseRepository<Cancellation>, CancellationRepository>();

builder.Services.AddScoped<ICrudEntityService<Service, ServiceCreate>, ServicesService>();
builder.Services.AddScoped<IServiceSlotsService, ServiceSlotsService>();
builder.Services.AddScoped<ICrudEntityService<Cancellation, CancellationCreate>, CancellationService>();

builder.Services.AddControllers();
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

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

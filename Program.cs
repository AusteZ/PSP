using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
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

// Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

// Repositories
builder.Services.AddScoped<IBaseRepository<Service>, ServicesRepository>();
builder.Services.AddScoped<IBaseRepository<ServiceSlot>, ServiceSlotsRepository>();
builder.Services.AddScoped<IBaseRepository<Cancellation>, CancellationRepository>();
builder.Services.AddScoped<IBaseRepository<Customer>, CustomersRepository>();

// Services
builder.Services.AddScoped<ICrudEntityService<Service, ServiceCreate>, ServicesService>();
builder.Services.AddScoped<IServiceSlotsService, ServiceSlotsService>();
builder.Services.AddScoped<ICrudEntityService<Cancellation, CancellationCreate>, CancellationService>();
builder.Services.AddScoped<ICustomersService, CustomersService>();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

using Microsoft.AspNetCore.Authentication.JwtBearer;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using PSP;
using PSP.Models;
using PSP.Models.DTOs;
using PSP.Models.Entities;
using PSP.Models.Entities.RelationalTables;
using PSP.Repositories;
using PSP.Services;
using PSP.Services.Interfaces;
using Microsoft.OpenApi.Models;

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

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});

var mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

// Repositories
builder.Services.AddScoped<IBaseRepository<Service>, ServicesRepository>();
builder.Services.AddScoped<IBaseRepository<ServiceSlot>, ServiceSlotsRepository>();
builder.Services.AddScoped<IBaseRepository<Cancellation>, CancellationRepository>();
builder.Services.AddScoped<IBaseRepository<Product>, ProductsRepository>();
builder.Services.AddScoped<IBaseRepository<Order>, OrdersRepository>();
builder.Services.AddScoped<IBaseRepository<OrderProduct>, OrderProductsRepository>();
builder.Services.AddScoped<IBaseRepository<Coupon>, CouponRepository>();
builder.Services.AddScoped<IBaseRepository<Discount>, DiscountRepository>();
builder.Services.AddScoped<IBaseRepository<ProductDiscount>, ProductDiscountsRepository>();
builder.Services.AddScoped<IBaseRepository<ServiceDiscount>, ServiceDiscountsRepository>();
builder.Services.AddScoped<IBaseRepository<Receipt>, ReceiptRepository>();
builder.Services.AddScoped<IBaseRepository<Customer>, CustomersRepository>();
builder.Services.AddScoped<IBaseRepository<Receipt>, ReceiptRepository>();

// Services
builder.Services.AddScoped<IServicesService, ServicesService>();
builder.Services.AddScoped<IServiceSlotsService, ServiceSlotsService>();
builder.Services.AddScoped<ICrudEntityService<Cancellation, CancellationCreate>, CancellationService>();
builder.Services.AddScoped<ICustomersService, CustomersService>();
builder.Services.AddScoped<IOrdersService, OrdersService>();
builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<ICrudEntityService<Coupon, CouponCreate>, CouponService>();
builder.Services.AddScoped<ICrudEntityService<Discount, DiscountCreate>, DiscountService>();
builder.Services.AddScoped<IPaymentService, PaymentsService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "PSP Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

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

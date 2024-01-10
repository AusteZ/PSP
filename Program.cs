using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PSP;
using PSP.Models;
using PSP.Models.DTOs;
using PSP.Models.Entities;
using PSP.Models.Entities.RelationalTables;
using PSP.Repositories;
using PSP.Services;
using PSP.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<PSPDatabaseContext>(opt =>
    opt.UseInMemoryDatabase("PSP"));

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});

var mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

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

builder.Services.AddScoped<IServicesService, ServicesService>();
builder.Services.AddScoped<IServiceSlotsService, ServiceSlotsService>();
builder.Services.AddScoped<ICrudEntityService<Cancellation, CancellationCreate>, CancellationService>();
builder.Services.AddScoped<ICrudEntityService<Order, OrderCreate>, OrdersService>();
builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<ICrudEntityService<Coupon, CouponCreate>, CouponService>();
builder.Services.AddScoped<ICrudEntityService<Discount, DiscountCreate>, DiscountService>();

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

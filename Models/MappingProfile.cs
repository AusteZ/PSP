using AutoMapper;
using PSP.Models.DTOs;
using PSP.Models.DTOs.Output;
using PSP.Models.Entities;
using PSP.Models.Entities.RelationalTables;

namespace PSP.Models;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Service, ServiceOutput>()
            .ForMember(dest => dest.Discounts, opt => opt.MapFrom(src => src.Discounts.Select(op => op.Discount))); ;
        CreateMap<ServiceCreate, Service>();
        CreateMap<Service, ServiceWithNoRelations>();
        CreateMap<ServiceDiscount, ServiceWithNoRelations>();

        CreateMap<ServiceSlotCreate, ServiceSlot>();
        CreateMap<ServiceSlot, ServiceSlotOutput>();
        CreateMap<ServiceSlot, ServiceSlotWithNoRelations>();
        CreateMap<ServiceSlot, ServiceSlotWithServiceOutput>();

        CreateMap<CancellationCreate, Cancellation>();

        CreateMap<Product, ProductOutput>()
            .ForMember(dest => dest.Orders, opt => opt.MapFrom(src => src.Orders.Select(op => op.Order)));
        CreateMap<Product, ProductOutput>()
            .ForMember(dest => dest.Discounts, opt =>opt.MapFrom(src => src.Discounts.Select(op => op.Discount)));
        CreateMap<ProductCreate, Product>();
        CreateMap<Product, ProductWithNoRelations>();
        CreateMap<OrderProduct, ProductWithQuantity>();
        CreateMap<ProductDiscount, ProductWithNoRelations>();

        CreateMap<Order, OrderOutput>().ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));
        CreateMap<OrderCreate, Order>();
        CreateMap<Order, OrderWithNoRelations>();

        CreateMap<CouponCreate, Coupon>();
        CreateMap<Coupon, CouponOutput>();

        CreateMap<Discount, DiscountOutput>()
            .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));
        CreateMap<Discount, DiscountOutput>()
            .ForMember(dest => dest.Services, opt => opt.MapFrom(src => src.Services));
        CreateMap<DiscountCreate, Discount>();
        CreateMap<Discount, DiscountWithNoRelations>();

    }
}
using AutoMapper;
using PSP.Models.DTOs;
using PSP.Models.DTOs.Output;
using PSP.Models.Entities;
using PSP.Models.Entities.RelationalTables;
using PSP.Services;

namespace PSP.Models;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Service, ServiceOutput>()
            .ForMember(dest => dest.Discounts, opt => opt.MapFrom(src => src.Discounts.Select(sd => sd.Discount)));
        CreateMap<ServiceCreate, Service>();
        CreateMap<Service, ServiceWithNoRelations>();

        CreateMap<ServiceSlotCreate, ServiceSlot>();
        CreateMap<ServiceSlot, ServiceSlotOutput>();
        CreateMap<ServiceSlot, ServiceSlotWithNoRelations>();
        CreateMap<ServiceSlot, ServiceSlotWithServiceOutput>()
            .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => DiscountService.GetLargestValid(src.Service.Discounts)));

        CreateMap<CancellationCreate, Cancellation>();

        CreateMap<Product, ProductOutput>()
            .ForMember(dest => dest.Orders, opt => opt.MapFrom(src => src.Orders.Select(op => op.Order)))
            .ForMember(dest => dest.Discounts, opt => opt.MapFrom(src => src.Discounts.Select(pd => pd.Discount)));
        CreateMap<ProductCreate, Product>();
        CreateMap<Product, ProductWithNoRelations>();
        CreateMap<OrderProduct, ProductWithDiscount>()
            .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => DiscountService.GetLargestValid(src.Product.Discounts)));

        CreateMap<Order, OrderOutput>()
            .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));
        CreateMap<OrderCreate, Order>();
        CreateMap<Order, OrderCreate>();
        CreateMap<OrderOutput, OrderCreate>().ForMember(dest => dest.serviceSlotIds, opt => opt.MapFrom(src => src.ServiceSlots.Select(op => op.Id))).ForMember(dest => dest.ProductsIds, opt => opt.MapFrom(src => src.Products.Select(op => op.Product.Id)));
        CreateMap<Order, OrderWithNoRelations>();

        CreateMap<CouponCreate, Coupon>();
        CreateMap<Coupon, CouponOutput>();

        CreateMap<Discount, DiscountOutput>()
            .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products.Select(pd => pd.Product)))
            .ForMember(dest => dest.Services, opt => opt.MapFrom(src => src.Services.Select(sd => sd.Service)));
        CreateMap<DiscountCreate, Discount>();
        CreateMap<Discount, DiscountWithNoRelations>();
        CreateMap<Receipt, ReceiptOutput>().ForMember(dest => dest.Order, opt => opt.MapFrom(s => s.Order));
    }
}
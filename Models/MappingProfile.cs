using AutoMapper;
using PSP.Models.DTOs;
using PSP.Models.Entities;

namespace PSP.Models;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Service, ServiceOutput>();
        CreateMap<ServiceSlot, ServiceSlotOfServiceOutput>();
        CreateMap<ServiceCreate, Service>();

        CreateMap<ServiceSlot, ServiceSlotOutput>();
        CreateMap<Service, ServiceOfServiceSlotOutput>();
        CreateMap<ServiceSlotCreate, ServiceSlot>();

        CreateMap<CancellationCreate, Cancellation>();

        CreateMap<Product, ProductOutput>();

        CreateMap<Order, OrderOutput>()
            .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products.Select(op => new ProductOfOrder
            {
                Id = op.Product.Id,
                Name = op.Product.Name,
                PriceEuros = op.Product.PriceEuros,
                ProductDescription = op.Product.ProductDescription
            })))
            .ForMember(dest => dest.ServiceSlots, opt => opt.MapFrom(src => src.ServiceSlots.Select(os => os.ServiceSlot)));
    }
}
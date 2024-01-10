﻿using AutoMapper;
using PSP.Models.DTOs;
using PSP.Models.DTOs.Output;
using PSP.Models.Entities;
using PSP.Models.Entities.RelationalTables;

namespace PSP.Models;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Service, ServiceOutput>();
        CreateMap<ServiceCreate, Service>();
        CreateMap<Service, ServiceWithNoRelations>();

        CreateMap<ServiceSlotCreate, ServiceSlot>();
        CreateMap<ServiceSlot, ServiceSlotOutput>();
        CreateMap<ServiceSlot, ServiceSlotWithNoRelations>();
        CreateMap<ServiceSlot, ServiceSlotWithServiceOutput>();

        CreateMap<CancellationCreate, Cancellation>();

        CreateMap<Product, ProductOutput>().ForMember(dest => dest.Orders, opt => opt.MapFrom(src => src.Orders.Select(op => op.Order)));
        CreateMap<ProductCreate, Product>();
        CreateMap<Product, ProductWithNoRelations>();
        CreateMap<OrderProduct, ProductWithQuantity>();

        CreateMap<Order, OrderOutput>().ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));
        CreateMap<OrderCreate, Order>();
        CreateMap<Order, OrderWithNoRelations>();
    }
}
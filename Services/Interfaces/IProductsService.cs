﻿using PSP.Models.DTOs;
using PSP.Models.Entities;

namespace PSP.Services.Interfaces
{
    public interface IProductsService : ICrudEntityService<Product, ProductCreate>
    {
        public void AddToOrder(int id, int orderId, int quantity);
        public void RemoveFromOrder(int id, int orderId, int quantity = -1);

        public void AddToDiscount(int id, int discountId);

        public void RemoveFromDiscount(int id, int discountId);
    }
}

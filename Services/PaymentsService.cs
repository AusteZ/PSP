using AutoMapper;
using PSP.Models.DTOs;
using PSP.Models.Entities;
using PSP.Models.Exceptions;
using PSP.Repositories;
using PSP.Services.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using PSP.Models.Entities.RelationalTables;

namespace PSP.Services
{
    public class PaymentsService : IPaymentService
    {
        private readonly IBaseRepository<Receipt> _receiptRepository;
        private readonly IOrdersService _orderService;
        private readonly ICrudEntityService<Coupon, CouponCreate> _couponService;
        private readonly ICustomersService _customersService;

        public PaymentsService(IBaseRepository<Receipt> repository, IOrdersService ordersService,
            ICrudEntityService<Coupon, CouponCreate> couponService, ICustomersService customersService) 
        {
            _receiptRepository = repository;
            _orderService = ordersService;
            _couponService = couponService;
            _customersService = customersService;
        }

        public Receipt PayWithCard(int orderId, CardPayment card, int? couponId )
        {
            var order = _orderService.Get(orderId);
            if (order.Status == PaymentStatus.completed)
                throw new UserFriendlyException("The order is already paid for", 400);

            if (!new CreditCardAttribute().IsValid(card.CardNumber))
                throw new UserFriendlyException("The card number is not valid", 400);
            if (!new Regex(@"^[0-9]{3,4}$").IsMatch(card.CVC))
                throw new UserFriendlyException("The card CVC is not valid", 400);

            ProcessPayment();

            return SaveReceipt(order, PaymentType.card, couponId);
        }

        public Receipt PayWithCash(int orderId, int? couponId)
        {
            var order = _orderService.Get(orderId);
            return SaveReceipt(order, PaymentType.cash, couponId);
        }

        private Receipt SaveReceipt(Order order, PaymentType type, int? couponId)
        {
            if (order.Status == PaymentStatus.completed)
                throw new UserFriendlyException("This order is already paid for", 400);

            order.Status = PaymentStatus.completed;
            _orderService.Update(order);

            var totalPrice = GetTotalPrice(order);

            var coupon = couponId != null ? _couponService.Get(couponId.Value) : null;
            if (coupon != null && !coupon.Used)
            {
                totalPrice = Math.Max(totalPrice - coupon.EuroPrice, 0);
                coupon.Used = true;
                _couponService.Update(coupon);
            }

            var loyaltyPointsUsed = Math.Min(order.LoyaltyPointsToUse, order.Customer.LoyaltyPoints);
            loyaltyPointsUsed = Math.Min(loyaltyPointsUsed, Convert.ToInt32(totalPrice * 100));
            totalPrice -= (float)loyaltyPointsUsed / 100;

            var receipt = new Receipt {
                OrderId = order.Id,
                Date = DateTime.Now,
                Total = totalPrice,
                Coupon = coupon,
                PaymentType = type,
            };
            _receiptRepository.Add(receipt);
            order.Customer.LoyaltyPoints -= loyaltyPointsUsed;
            order.Customer.LoyaltyPoints += Convert.ToInt32(totalPrice);
            _customersService.Update(order.Customer);
            return receipt;
        }

        private float GetTotalPrice(Order order)
        {
            float totalPrice = 0;
            foreach (var serviceSlot in order.ServiceSlots)
                totalPrice += serviceSlot.Service.EuroCost * GetDiscountMultiplier(serviceSlot);
            foreach (var product in order.Products)
                totalPrice += product.Product.PriceEuros * GetDiscountMultiplier(product) * product.Quantity;

            return totalPrice;
        }

        private float GetDiscountMultiplier(ServiceSlot serviceSlot)
        {
            var maxDiscount = DiscountService.GetLargestValid(serviceSlot.Service.Discounts);
            if (maxDiscount == null)
                return 1;

            return 1 - (float)maxDiscount.Percentage / 100;
        }

        private float GetDiscountMultiplier(OrderProduct product)
        {
            var maxDiscount = DiscountService.GetLargestValid(product.Product.Discounts);
            if (maxDiscount == null)
                return 1;

            return 1 - (float)maxDiscount.Percentage / 100;
        }

        private void ProcessPayment()
        {
            //To connect some payment gateway
        }

        public IEnumerable<Receipt> GetAll()
        {
            return _receiptRepository.FindAll();
        }

        public Receipt Get(int id)
        {
            if (_orderService.Get(id).Status != PaymentStatus.completed)
                throw new UserFriendlyException($"Order with '{id}' is not completed.", 400);
            var entity = _receiptRepository.Find(id);
            if(entity == null)
                throw new UserFriendlyException($"Entity of type 'Receipt' with order id '{id}' was not found.", 404);
            return entity!;
        }
    }
}

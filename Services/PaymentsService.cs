using AutoMapper;
using PSP.Models.DTOs;
using PSP.Models.DTOs.Output;
using PSP.Models.DTOs.Payments;
using PSP.Models.Entities;
using PSP.Models.Exceptions;
using PSP.Repositories;
using PSP.Services.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace PSP.Services
{
    public class PaymentsService : IPaymentService
    {
        private readonly IBaseRepository<Receipt> _receiptRepository;
        private readonly ICrudEntityService<Order, OrderCreate> _orderService;
        private readonly ICrudEntityService<Coupon, CouponCreate> _couponService;
        private readonly IProductsService _productsService;
        private readonly IServicesService _servicesService;
        private readonly IMapper _mapper;

        public PaymentsService(IBaseRepository<Receipt> repository, ICrudEntityService<Order, OrderCreate> ordersService,  ICrudEntityService<Coupon, CouponCreate> couponService, IProductsService productsService, IServicesService servicesService, IMapper mapper) 
        {
            _receiptRepository = repository;
            _orderService = ordersService;
            _couponService = couponService;
            _productsService = productsService;
            _servicesService = servicesService;
            _mapper = mapper;
        }

        public ReceiptOutput PayWithCard(Order order, CardPayment card, int? couponId )
        {
            if (order.Status == PaymentStatus.completed)
                throw new UserFriendlyException("The order is already paid for", 400);

            if (new CreditCardAttribute().IsValid(card.CardNumber))
                throw new UserFriendlyException("The card number is not valid", 400);
            if (new Regex(@"^[0-9]{3,4}$").IsMatch(card.CVC))
                throw new UserFriendlyException("The card CVC is not valid", 400);

            ProcessPayment();

            return SaveReceipt(order, PaymentType.card, couponId);
        }

        public ReceiptOutput PayWithCash(Order order, int? couponId)
        {
            return SaveReceipt(order, PaymentType.cash, couponId);
        }

        private void ProcessPayment()
        {
            //To connect some payment gateway
        }

        private ReceiptOutput SaveReceipt(Order order, PaymentType type, int? couponId)
        {
            order.Status = PaymentStatus.completed;

            _orderService.Update(_mapper.Map<OrderCreate>(order), order.Id);

            var servicesDiscounts = order.ServiceSlots.Select(x => x.Service.EuroCost * MultiplyAllDiscounts(x.Service.Discounts.Select(x => x.Discount).Where(x => DateTime.Compare(x.StartDate, DateTime.Now) <= 0 && DateTime.Compare(x.EndDate, DateTime.Now) >= 0).Select(x => x.Percentage / 100.0))).Sum();

            var productDiscounts = order.Products.Select(x => x.Product.PriceEuros * x.Quantity * MultiplyAllDiscounts(x.Product.Discounts.Select(x => x.Discount).Where(x => DateTime.Compare(x.StartDate, DateTime.Now) <= 0 && DateTime.Compare(x.EndDate, DateTime.Now) >= 0).Select(x => x.Percentage / 100.0))).Sum();

            Coupon? coupon = couponId == null ? _mapper.Map<Coupon>(couponId) : null;
            var receipt = new Receipt() {
                OrderId = order.Id,
                Date = DateTime.Now,
                Total = (decimal)(servicesDiscounts + productDiscounts + order.Tips - (coupon != null ? coupon.EuroPrice : 0)),
                Coupon = coupon,
                PaymentType = type,
            };
            _receiptRepository.Add(receipt);
            var receiptOutput = _mapper.Map<ReceiptOutput>(receipt);
            receiptOutput.Order = _mapper.Map<OrderOutput>(order);
            receiptOutput.PaymentType = type.ToString();

            return receiptOutput;
        }
        private double MultiplyAllDiscounts(IEnumerable<double> percentages)
        {
            double product = 1.0;
            foreach (var percentage in percentages)
            {
                product *= (1-percentage);
            }
            return product;
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

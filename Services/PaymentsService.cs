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
        private readonly IMapper _mapper;

        public PaymentsService(IBaseRepository<Receipt> repository, ICrudEntityService<Order, OrderCreate> ordersService, IMapper mapper) 
        {
            _receiptRepository = repository;
            _orderService = ordersService;
            _mapper = mapper;
        }

        public ReceiptOutput PayWithCard(OrderOutput order, CardPayment card, float tip )
        {
            if (new CreditCardAttribute().IsValid(card.CardNumber))
                throw new UserFriendlyException("The card number is not valid", 400);
            if (new Regex(@"^[0-9]{3,4}$").IsMatch(card.CVC))
                throw new UserFriendlyException("The card CVC is not valid", 400);

            if(order.Status == PaymentStatus.completed)
                throw new UserFriendlyException("The order is already paid for", 400);

            ProcessPayment();

            return SaveReceipt(order, PaymentType.card, tip);
        }

        public ReceiptOutput PayWithCash(OrderOutput order, float tip)
        {
            return SaveReceipt(order, PaymentType.cash, tip);
        }

        private void ProcessPayment()
        {
            //To connect some payment gateway
        }

        private ReceiptOutput SaveReceipt(OrderOutput order, PaymentType type, float tip)
        {
            order.Status = PaymentStatus.completed;

            _orderService.Update(_mapper.Map<OrderCreate>(order), order.Id);//TODO Avoid unnecessary mapping, but even if Order update exists, there probably will still be a cast from OrderOutput to Order

            var receipt = new Receipt() {
                OrderId = order.Id,
                Date = DateTime.Now,
                Tip = (decimal)tip,
                Total = (decimal)(order.ServiceSlots.Select(x => x.Service.EuroCost).Concat(order.Products.Select(x => x.Product.PriceEuros * x.Quantity)).Sum() + tip),
                PaymentType = type,
            };
            _receiptRepository.Add(receipt);
            var receiptOutput = _mapper.Map<ReceiptOutput>(receipt);
            receiptOutput.Order = order;
            receiptOutput.PaymentType = type.ToString();

            return receiptOutput;
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

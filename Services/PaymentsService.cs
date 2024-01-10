using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PSP.Models.DTOs;
using PSP.Models.DTOs.Output;
using PSP.Models.DTOs.Payments;
using PSP.Models.Entities;
using PSP.Models.Entities.RelationalTables;
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

        public ReceiptOutput PayWithCard(OrderOutput order, CardPayment card)
        {
            /*if (!new CreditCardAttribute().IsValid(card.CardNumber))
                throw new UserFriendlyException("The card number is not valid", 400);*/
            /*if (new Regex(@"^[0-9]{3,4}$").IsMatch(card.CVC))
                throw new UserFriendlyException("The card CVC is not valid", 400);*/

            if(order.Status == PaymentStatus.paid)
                throw new UserFriendlyException("The order is already paid for", 400);

            ProcessPayment();

            return SaveReceipt(order, PaymentType.card);
            
        }
        public ReceiptOutput PayWithCash(OrderOutput order)
        {
            return SaveReceipt(order, PaymentType.cash);
        }
        private void ProcessPayment()
        {
            //To connect some payment gateway
        }
        private ReceiptOutput SaveReceipt(OrderOutput order, PaymentType type)
        {
            order.Status = PaymentStatus.paid;

            _orderService.Update(_mapper.Map<OrderCreate>(order), order.Id);

            var receipt = new Receipt() {
                OrderId = order.Id,
                Date = DateTime.Now,
                Total = (decimal)order.ServiceSlots.Select(x => x.Service.EuroCost).Concat(order.Products.Select(x => x.PriceEuros)).Sum(),
                PaymentType = type,
            };
            _receiptRepository.Add(receipt);
            var receiptOutput = _mapper.Map<ReceiptOutput>(receipt);
            receiptOutput.Order = order;
            receiptOutput.PaymentType = type.ToString();

            return receiptOutput;
        }

    }
}

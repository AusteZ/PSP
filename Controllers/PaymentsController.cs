using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PSP.Models.DTOs;
using PSP.Models.DTOs.Output;
using PSP.Models.DTOs.Payments;
using PSP.Models.Entities;
using PSP.Services;
using PSP.Services.Interfaces;

namespace PSP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentsController : Controller
    {
        private readonly IPaymentService _service;
        private readonly ICrudEntityService<Order, OrderCreate> _orderService;
        private readonly IMapper _mapper;

        public PaymentsController(IPaymentService service, ICrudEntityService<Order, OrderCreate> orderService, IMapper mapper)
        {
            _service = service;
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpPost("card")]
        public ActionResult PayWithCard(int orderId, float tip, [FromBody] CardPayment card)
        {
            return Ok(_service.PayWithCard(_mapper.Map<OrderOutput>(_orderService.Get(orderId)), card, tip));
        }

        [HttpPost("cash")]
        public ActionResult PayWithCash(int orderId, float tip)
        {
            return Ok(_service.PayWithCash(_mapper.Map<OrderOutput>(_orderService.Get(orderId)), tip));
        }

        [HttpGet("Receipt/{orderId}")]
        public ActionResult GetOrderReceipt(int orderId)
        {
            return Ok(_mapper.Map<ReceiptOutput>(_service.Get(orderId)));
        }

        [HttpGet("Receipt")]
        public ActionResult GetAllOrderReceipts()
        {
            return Ok(_mapper.Map<IEnumerable<ReceiptOutput>>(_service.GetAll()));
        }
    }
}

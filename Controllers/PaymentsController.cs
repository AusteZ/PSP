using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PSP.Models.DTOs;
using PSP.Models.DTOs.Output;
using PSP.Models.Entities;
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
        public ActionResult PayWithCard(int orderId, [FromBody] CardPayment card, int? couponId = null)
        {
            return Ok(_mapper.Map<ReceiptOutput>(_service.PayWithCard(orderId, card, couponId)));
        }

        [HttpPost("cash")]
        public ActionResult PayWithCash(int orderId, int? couponId = null)
        {
            return Ok(_mapper.Map<ReceiptOutput>(_service.PayWithCash(orderId, couponId)));
        }

        [HttpGet("Receipt/{orderId}")]
        public ActionResult GetOrderReceipt(int orderId)
        {
            return Ok(_mapper.Map<ReceiptOutput>(_service.Get(orderId)));
        }

        // TODO: no orders showing up, need to fix
        [HttpGet("Receipt")]
        public ActionResult GetAllOrderReceipts()
        {
            return Ok(_mapper.Map<IEnumerable<ReceiptOutput>>(_service.GetAll()));
        }
    }
}

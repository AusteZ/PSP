using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PSP.Models.DTOs;
using PSP.Models.DTOs.Output;
using PSP.Models.Entities;
using PSP.Services.Interfaces;

namespace PSP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServiceSlotsController : Controller
    {
        private readonly IServiceSlotsService _service;
        private readonly ICrudEntityService<Cancellation, CancellationCreate> _cancellationEntityService;
        private readonly IMapper _mapper;

        public ServiceSlotsController(IServiceSlotsService service, ICrudEntityService<Cancellation, CancellationCreate> cancellationEntityService, IMapper mapper)
        {
            _service = service;
            _cancellationEntityService = cancellationEntityService;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public ActionResult GetAll([FromQuery] int? employeeIdFilter, [FromQuery] int? serviceIdFilter, [FromQuery] bool? isFree)
        {
            return Ok(_mapper.Map<IEnumerable<ServiceSlotOutput>>(_service.GetFiltered(employeeIdFilter, serviceIdFilter, isFree)));
        }

        [Authorize]
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            return Ok(_mapper.Map<ServiceSlotOutput>(_service.Get(id)));
        }

        [Authorize]
        [HttpPost]
        public ActionResult Post([FromBody] ServiceSlotCreate body)
        {
            return Ok(_mapper.Map<ServiceSlotOutput>(_service.Add(body)));
        }

        [Authorize]
        [HttpPost("{id}/book")]
        public ActionResult Book(int id, [FromQuery] int orderId)
        {
            _service.Book(id, orderId);
            return Ok();
        }

        [Authorize]
        [HttpGet("cancellations")]
        public ActionResult GetCancellations()
        {
            return Ok(_cancellationEntityService.GetAll());
        }

        [Authorize]
        [HttpPost("{id}/cancel")]
        public ActionResult Cancel(int id)
        {
            _service.Cancel(id);
            return Ok();
        }

        [Authorize]
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] ServiceSlotCreate body)
        {
            return Ok(_mapper.Map<ServiceSlotOutput>(_service.Update(body, id)));
        }

        [Authorize]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _service.Delete(id);
            return Ok();
        }
    }
}

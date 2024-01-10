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

        [HttpGet]
        public ActionResult GetAll([FromQuery] int? employeeIdFilter, [FromQuery] int? serviceIdFilter, [FromQuery] bool? isFree)
        {
            return Ok(_mapper.Map<IEnumerable<ServiceSlotOutput>>(_service.GetFiltered(employeeIdFilter, serviceIdFilter, isFree)));
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            return Ok(_mapper.Map<ServiceSlotOutput>(_service.Get(id)));
        }

        [HttpPost]
        public ActionResult Post([FromBody] ServiceSlotCreate body)
        {
            return Ok(_mapper.Map<ServiceSlotOutput>(_service.Add(body)));
        }

        [HttpPost("{id}/book")]
        public ActionResult Book(int id, [FromQuery] int orderId)
        {
            _service.Book(id, orderId);
            return Ok();
        }

        [HttpGet("cancellations")]
        public ActionResult GetCancellations()
        {
            return Ok(_cancellationEntityService.GetAll());
        }

        [HttpPost("{id}/cancel")]
        public ActionResult Cancel(int id)
        {
            _service.Cancel(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] ServiceSlotCreate body)
        {
            return Ok(_mapper.Map<ServiceSlotOutput>(_service.Update(body, id)));
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _service.Delete(id);
            return Ok();
        }
    }
}

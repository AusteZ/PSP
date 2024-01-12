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

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult GetAll([FromQuery] int? employeeIdFilter, [FromQuery] int? serviceIdFilter, [FromQuery] bool? isFree)
        {
            return Ok(_mapper.Map<IEnumerable<ServiceSlotOutput>>(_service.GetFiltered(employeeIdFilter, serviceIdFilter, isFree)));
        }

        [HttpGet("{id}")]
        [Authorize]
        public ActionResult Get(int id)
        {
            return Ok(_mapper.Map<ServiceSlotOutput>(_service.Get(id)));
        }

        [HttpPost]
        [Authorize]
        public ActionResult Post([FromBody] ServiceSlotCreate body)
        {
            return Ok(_mapper.Map<ServiceSlotOutput>(_service.Add(body)));
        }

        [HttpPost("{id}/book")]
        [Authorize]
        public ActionResult Book(int id, [FromQuery] int orderId)
        {
            _service.Book(id, orderId);
            return Ok();
        }

        [HttpGet("cancellations")]
        [Authorize]
        public ActionResult GetCancellations()
        {
            return Ok(_cancellationEntityService.GetAll());
        }

        [HttpPost("{id}/cancel")]
        [Authorize]
        public ActionResult Cancel(int id)
        {
            _service.Cancel(id);
            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize]
        public ActionResult Put(int id, [FromBody] ServiceSlotCreate body)
        {
            return Ok(_mapper.Map<ServiceSlotOutput>(_service.Update(body, id)));
        }

        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult Delete(int id)
        {
            _service.Delete(id);
            return Ok();
        }
    }
}

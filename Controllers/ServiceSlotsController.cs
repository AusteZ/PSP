using Microsoft.AspNetCore.Mvc;
using PSP.Models;
using PSP.Models.RequestBodies;
using PSP.Services;
using PSP.Services.Interfaces;

namespace PSP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServiceSlotsController : Controller
    {
        private readonly PSPDatabaseContext _db;
        private readonly IServiceSlotsService _service;
        private readonly IBaseService<Cancellation, CancellationCreate> _cancellationService;

        public ServiceSlotsController(PSPDatabaseContext db, IServiceSlotsService service, IBaseService<Cancellation, CancellationCreate> cancellationService)
        {
            _db = db;
            _service = service;
            _cancellationService = cancellationService;
        }

        [HttpGet]
        public ActionResult GetAll([FromQuery] int? employeeIdFilter, [FromQuery] int? serviceIdFilter, [FromQuery] bool? isFree)
        {
            return Ok(_service.GetFiltered(employeeIdFilter, serviceIdFilter, isFree));
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            return Ok(_service.Get(id));
        }

        [HttpPost]
        public ActionResult Post([FromBody] ServiceSlotCreate body)
        {
            _service.Add(body);
            return Ok();
        }

        [HttpPost("{id}/book")]
        public ActionResult Book(int id, [FromBody] ServiceSlotBooking body)
        {
            _service.Book(id, body);
            return Ok();
        }

        [HttpGet("cancellations")]
        public ActionResult GetCancellations()
        {
            return Ok(_cancellationService.GetAll());
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
            _service.Update(body, id);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _service.Delete(id);
            return Ok();
        }
    }
}

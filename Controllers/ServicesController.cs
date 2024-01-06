using Microsoft.AspNetCore.Mvc;
using PSP.Models;
using PSP.Services;

namespace PSP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServicesController : Controller
    {
        private readonly ServicesService _service;

        public ServicesController(ServicesService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            return Ok(_service.Get(id));
        }

        [HttpPost]
        public ActionResult Post([FromBody] ServiceCreate body)
        {
            return Ok(_service.Add(body));
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody]ServiceCreate body)
        {
            return Ok(_service.Update(body, id));
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _service.Delete(id);
            return NoContent();
        }
    }
}

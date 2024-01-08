using Microsoft.AspNetCore.Mvc;
using PSP.Models.DTOs;
using PSP.Models.Entities;
using PSP.Services.Interfaces;

namespace PSP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServicesController : Controller
    {
        private readonly ICrudEntityService<Service, ServiceCreate> _entityService;

        public ServicesController(ICrudEntityService<Service, ServiceCreate> entityService)
        {
            _entityService = entityService;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok(_entityService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            return Ok(_entityService.Get(id));
        }

        [HttpPost]
        public ActionResult Post([FromBody] ServiceCreate body)
        {
            return Ok(_entityService.Add(body));
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody]ServiceCreate body)
        {
            return Ok(_entityService.Update(body, id));
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _entityService.Delete(id);
            return NoContent();
        }
    }
}

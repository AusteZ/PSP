using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PSP.Models.DTOs;
using PSP.Models.Entities;
using PSP.Services;
using PSP.Services.Interfaces;

namespace PSP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : Controller
    {
        private readonly ICrudEntityService<Order, OrderCreate> _service;
        public OrdersController(ICrudEntityService<Order, OrderCreate> service) 
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        // GET: orders/:id
        [HttpGet("{id}")]
        public ActionResult Index(int id)
        {
            return Ok(_service.Get(id));
        }

        // POST: orders
        [HttpPost]
        public ActionResult Post([FromBody] OrderCreate body)
        {
            return Ok(_service.Add(body));
        }

        // PUT: orders/:orderID
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] OrderCreate body)
        {
            return Ok(_service.Update(body, id));
        }

        // DELETE: orders/:orderID
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _service.Delete(id);
            return NoContent();
        }

        // PATCH: orders/:orderID
        [HttpPatch("{id}")]
        public ActionResult Patch(int id, [FromBody] JsonPatchDocument<Order> orderDocument)
        {
            return Ok(_service.Patch(orderDocument, id));
        }
    }
}

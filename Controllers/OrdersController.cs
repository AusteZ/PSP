using Microsoft.AspNetCore.Mvc;
using PSP.Models.Entities;

namespace PSP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : Controller
    {
        private readonly PSPDatabaseContext _db;

        public OrdersController(PSPDatabaseContext db) 
        {
            _db = db;
        }

        // GET: Orders/:id
        [HttpGet("{id}")]
        public ActionResult Index(int id)
        {
            Order? order = _db.Orders.Find(id);

            if (order != null) 
            {
                return Ok(order);
            }
            else
            {
                return StatusCode(404);
            }
        }

        // POST: Orders
        [HttpPost]
        public ActionResult Post(IFormCollection requestParams)
        {
            try
            {
                Order order = new Order();
                order.PaymentStatus = requestParams["paymentStatus"];

                _db.Orders.Add(order);
                _db.SaveChanges();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        // PUT: Orders/:id
        [HttpPut("{id}")]
        public ActionResult Put(int id, IFormCollection collection)
        {
            try
            {
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        // DELETE: Orders/:id
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            return Ok();
        }
    }
}

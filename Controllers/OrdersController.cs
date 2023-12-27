using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PSP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : Controller
    {
        // GET: Orders/:id
        [HttpGet("{id}")]
        public ActionResult Index(int id)
        {
            return Ok(id);
        }

        // POST: Orders
        [HttpPost]
        public ActionResult Post(IFormCollection collection)
        {
            try
            {
                return Ok("POST Lol!");
            }
            catch
            {
                return Ok("NOT Lol!");
            }
        }

        // PUT: Orders/:id
        [HttpPut("{id}")]
        public ActionResult Put(int id, IFormCollection collection)
        {
            try
            {
                return Ok("PUT Lol!");
            }
            catch
            {
                return Ok("PUT NOT LOL");
            }
        }

        // DELETE: Orders/:id
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            return Ok("deleted!");
        }
    }
}

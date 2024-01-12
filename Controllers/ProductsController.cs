using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PSP.Models.DTOs;
using PSP.Models.DTOs.Output;
using PSP.Models.Entities;
using PSP.Services;
using PSP.Services.Interfaces;

namespace PSP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductsService _service;
        private readonly IMapper _mapper;

        public ProductsController(IProductsService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult GetAll()
        {
            return Ok(_mapper.Map<IEnumerable<ProductOutput>>(_service.GetAll()));
        }

        [HttpGet("{id}")]
        [Authorize]
        public ActionResult Get(int id)
        {
            return Ok(_mapper.Map<ProductOutput>(_service.Get(id)));
        }

        [HttpPost]
        [Authorize]
        public ActionResult Post([FromBody] ProductCreate body)
        {
            return Ok(_mapper.Map<ProductOutput>(_service.Add(body)));
        }

        [HttpPut("{id}")]
        [Authorize]
        public ActionResult Put(int id, [FromBody] ProductCreate body)
        {
            return Ok(_mapper.Map<ProductOutput>(_service.Update(body, id)));
        }

        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult Delete(int id)
        {
            _service.Delete(id);
            return NoContent();
        }

        [HttpPost("{id}/addToOrder")]
        [Authorize]
        public ActionResult AddToOrder(int id, [FromQuery] int orderId, [FromQuery] int quantity = 1)
        {
            _service.AddToOrder(id, orderId, quantity);
            return Ok();
        }

        [HttpPost("{id}/removeFromOrder")]
        [Authorize]
        public ActionResult RemoveFromOrder(int id, [FromQuery] int orderId, [FromQuery] int quantity = 1)
        {
            _service.RemoveFromOrder(id, orderId, quantity);
            return Ok();
        }
    }
}
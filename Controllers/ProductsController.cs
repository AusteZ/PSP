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

        [Authorize]
        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok(_mapper.Map<IEnumerable<ProductOutput>>(_service.GetAll()));
        }

        [Authorize]
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            return Ok(_mapper.Map<ProductOutput>(_service.Get(id)));
        }

        [Authorize]
        [HttpPost]
        public ActionResult Post([FromBody] ProductCreate body)
        {
            return Ok(_mapper.Map<ProductOutput>(_service.Add(body)));
        }

        [Authorize]
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] ProductCreate body)
        {
            return Ok(_mapper.Map<ProductOutput>(_service.Update(body, id)));
        }

        [Authorize]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _service.Delete(id);
            return NoContent();
        }

        [Authorize]
        [HttpPost("{id}/addToOrder")]
        public ActionResult AddToOrder(int id, [FromQuery] int orderId, [FromQuery] int quantity = 1)
        {
            _service.AddToOrder(id, orderId, quantity);
            return Ok();
        }

        [Authorize]
        [HttpPost("{id}/removeFromOrder")]
        public ActionResult RemoveFromOrder(int id, [FromQuery] int orderId, [FromQuery] int quantity = 1)
        {
            _service.RemoveFromOrder(id, orderId, quantity);
            return Ok();
        }
    }
}
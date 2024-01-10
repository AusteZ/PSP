using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PSP.Models.DTOs;
using PSP.Models.Entities;
using PSP.Services;
using PSP.Services.Interfaces;

namespace PSP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : Controller
    {
        private readonly ICrudEntityService<Product, ProductCreate> _service;
        private readonly IMapper _mapper;

        public ProductsController(ICrudEntityService<Product, ProductCreate> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok(_mapper.Map<IEnumerable<ProductOutput>>(_service.GetAll()));
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            return Ok(_mapper.Map<ProductOutput>(_service.Get(id)));
        }

        [HttpPost]
        public ActionResult Post([FromBody] ProductCreate body)
        {
            return Ok(_mapper.Map<ProductOutput>(_service.Add(body)));
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] ProductCreate body)
        {
            return Ok(_mapper.Map<ProductOutput>(_service.Update(body, id)));
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _service.Delete(id);
            return NoContent();
        }
    }
}
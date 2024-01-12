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
    public class DiscountsController : Controller
    { 
        private readonly ICrudEntityService<Discount, DiscountCreate> _service;
        private readonly IMapper _mapper;

        public DiscountsController(ICrudEntityService<Discount, DiscountCreate> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult GetAll()
        {
            return Ok(_mapper.Map<IEnumerable<DiscountOutput>>(_service.GetAll()));
        }

        [HttpGet("{id}")]
        [Authorize]
        public ActionResult Get(int id)
        {
            return Ok(_mapper.Map<DiscountOutput>(_service.Get(id)));
        }

        [HttpPost]
        [Authorize]
        public ActionResult Post([FromBody] DiscountCreate body)
        {
            var test = _mapper.Map<DiscountOutput>(_service.Add(body));
            return Ok(test);
        }

        [HttpPut("{id}")]
        [Authorize]
        public ActionResult Put(int id, [FromBody] DiscountCreate body)
        {
            return Ok(_mapper.Map<DiscountOutput>(_service.Update(body, id)));
        }

        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult Delete(int id)
        {
            _service.Delete(id);
            return NoContent();
        }
    }
}

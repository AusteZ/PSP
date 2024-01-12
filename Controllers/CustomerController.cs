using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PSP.Models.DTOs;
using PSP.Models.DTOs.Output;
using PSP.Services.Interfaces;

namespace PSP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : Controller
    {
        private readonly ICustomersService _service;
        private readonly IMapper _mapper;

        public CustomerController(ICustomersService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok(_mapper.Map<IEnumerable<CustomerOutput>>(_service.GetAll()));
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            return Ok(_mapper.Map<CustomerOutput>(_service.Get(id)));
        }

        [HttpPost]
        public ActionResult Post([FromBody] CustomerCreate body)
        {
            return Ok(_mapper.Map<CustomerOutput>(_service.Add(body)));
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] CustomerCreate body)
        {
            return Ok(_mapper.Map<CustomerOutput>(_service.Update(body, id)));
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _service.Delete(id);
            return NoContent();
        }

    }
}

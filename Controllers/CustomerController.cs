using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "admin")]
        public ActionResult GetAll()
        {
            return Ok(_mapper.Map<IEnumerable<CustomerOutput>>(_service.GetAll()));
        }

        [HttpGet("{id}")]
        [Authorize]
        public ActionResult Get(int id)
        {
            return Ok(_mapper.Map<CustomerOutput>(_service.Get(id)));
        }

        [HttpPost]
        public ActionResult Post([FromBody] CustomerCreate body)
        {
            if (!User.IsInRole("admin") && body.Role == "admin")
            {
                return Forbid();
            }

            return Ok(_mapper.Map<CustomerOutput>(_service.Add(body)));
        }

        [HttpPut("{id}")]
        [Authorize]
        public ActionResult Put(int id, [FromBody] CustomerCreate body)
        {
            return Ok(_mapper.Map<CustomerOutput>(_service.Update(body, id)));
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

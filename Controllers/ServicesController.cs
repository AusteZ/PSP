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
    public class ServicesController : Controller
    {
        private readonly IServicesService _service;
        private readonly IMapper _mapper;

        public ServicesController(IServicesService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok(_mapper.Map<IEnumerable<ServiceOutput>>(_service.GetAll()));
        }

        [Authorize]
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            return Ok(_mapper.Map<ServiceOutput>(_service.Get(id)));
        }

        [Authorize]
        [HttpPost]
        public ActionResult Post([FromBody] ServiceCreate body)
        {
            return Ok(_mapper.Map<ServiceOutput>(_service.Add(body)));
        }

        [Authorize]
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody]ServiceCreate body)
        {
            return Ok(_mapper.Map<ServiceOutput>(_service.Update(body, id)));
        }

        [Authorize]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _service.Delete(id);
            return NoContent();
        }
    }
}

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
    public class CouponController : Controller
    {
        private readonly ICrudEntityService<Coupon, CouponCreate> _service;
        private readonly IMapper _mapper;

        public CouponController(ICrudEntityService<Coupon, CouponCreate> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok(_mapper.Map<IEnumerable<CouponOutput>>(_service.GetAll()));
        }

        [Authorize]
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            return Ok(_mapper.Map<CouponOutput>(_service.Get(id)));
        }

        [Authorize]
        [HttpPost]
        public ActionResult Post([FromBody] CouponCreate body)
        {
            return Ok(_mapper.Map<CouponOutput>(_service.Add(body)));
        }

        [Authorize]
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] CouponCreate body)
        {
            return Ok(_mapper.Map<CouponOutput>(_service.Update(body, id)));
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

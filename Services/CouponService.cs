using AutoMapper;
using PSP.Models.DTOs;
using PSP.Models.Entities;
using PSP.Repositories;

namespace PSP.Services
{
    public class CouponService : CrudEntityService<Coupon, CouponCreate>
    {
        public CouponService(IBaseRepository<Coupon> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        protected override Coupon ModelToEntity(CouponCreate entity, int id = 0)
        {
            var coupon = _mapper.Map<Coupon>(entity);
            coupon.Id = id;
            return coupon;
        }
    }
}

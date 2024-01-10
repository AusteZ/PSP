using PSP.Models.Entities;

namespace PSP.Repositories
{
    public class CouponRepository : BaseRepository<Coupon>
    {
        public CouponRepository(PSPDatabaseContext dbContext) : base(dbContext){ }
    }
}

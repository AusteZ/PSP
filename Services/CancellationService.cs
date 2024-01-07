using PSP.Models;
using PSP.Models.RequestBodies;
using PSP.Repositories;

namespace PSP.Services
{
    public class CancellationService : BaseService<Cancellation, CancellationCreate>
    {
        public CancellationService(IBaseRepository<Cancellation> repository) : base(repository)
        { }

        protected override Cancellation ModelToEntity(CancellationCreate entity, int id = 0)
        {
            return new Cancellation()
            {
                Id = id,
                CancellationTime = entity.CancellationTime,
                ServiceSlotId = entity.ServiceSlotId,
                CustomerId = entity.CustomerId
            };
        }
    }
}

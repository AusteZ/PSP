using PSP.Models.DTOs;
using PSP.Models.Entities;
using PSP.Repositories;

namespace PSP.Services
{
    public class CancellationService : CrudEntityService<Cancellation, CancellationCreate>
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

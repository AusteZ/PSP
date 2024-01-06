using PSP.Models;
using PSP.Models.RequestBodies;

namespace PSP.Services
{
    public class CancellationService : BaseService<Cancellation, CancellationCreate>
    {
        public CancellationService(PSPDatabaseContext db) : base(db)
        { }

        protected override Cancellation ModelToEntity(CancellationCreate entity, int id = 0)
        {
            return new Cancellation()
            {
                Id = id,
                CancellationTime = entity.CancellationTime,
                ServiceSlotId = entity.ServiceSlotId,
            };
        }
    }
}

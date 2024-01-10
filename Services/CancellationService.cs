using AutoMapper;
using PSP.Models.DTOs;
using PSP.Models.Entities;
using PSP.Repositories;

namespace PSP.Services
{
    public class CancellationService : CrudEntityService<Cancellation, CancellationCreate>
    {
        public CancellationService(IBaseRepository<Cancellation> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        protected override Cancellation ModelToEntity(CancellationCreate entity, int id = 0)
        {
             var cancellation = _mapper.Map<Cancellation>(entity);
             cancellation.Id = id;
             return cancellation;
        }
    }
}

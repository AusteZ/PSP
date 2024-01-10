﻿using Microsoft.EntityFrameworkCore;
using PSP.Models.Entities;

namespace PSP.Repositories
{
    public class ServicesRepository : BaseRepository<Service>
    {
        public ServicesRepository(PSPDatabaseContext dbContext) : base(dbContext) { }

        public override Service? Find(params int[] ids)
        {
            return _dbSet.Include(e => e.ServiceSlots).FirstOrDefault(e => e.Id == ids[0]);
        }

        public override IEnumerable<Service> FindAll()
        {
            return _dbSet.Include(e => e.ServiceSlots).ToList();
        }
    }
}

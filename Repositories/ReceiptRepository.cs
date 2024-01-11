using PSP.Models.Entities;

namespace PSP.Repositories
{
    public class ReceiptRepository : BaseRepository<Receipt>
    {
        public ReceiptRepository(PSPDatabaseContext dbContext) : base(dbContext) { }

        public override Receipt? Find(params int[] ids)
        {
            return _dbSet.Find(ids[0]);
        }
    }
}

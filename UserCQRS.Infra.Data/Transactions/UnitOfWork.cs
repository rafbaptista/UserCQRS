using System.Threading.Tasks;
using UserCQRS.Domain.Interfaces.Transactions;
using UserCQRS.Infra.Data.Context;

namespace UserCQRS.Infra.Data.Transactions
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UserCQRSContext _context;

        public UnitOfWork(UserCQRSContext context)
        {
            _context = context;
        }

        public async Task<bool> Commit()
        {
            bool result = await (_context.SaveChangesAsync()) > 0;
            return result;
        }

        public void Rollback()
        {
            
        }
    }
}

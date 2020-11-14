using System.Threading.Tasks;

namespace UserCQRS.Domain.Interfaces.Transactions
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
        void Rollback();
    }
}

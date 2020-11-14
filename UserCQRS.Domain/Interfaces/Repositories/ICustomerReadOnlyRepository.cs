using System.Collections.Generic;
using System.Threading.Tasks;
using UserCQRS.Domain.Entities;

namespace UserCQRS.Domain.Interfaces.Repositories
{
    public interface ICustomerReadOnlyRepository
    {                        
        Task<IEnumerable<Customer>> GetAll();
        Task<Customer> GetById(int id);
        Task<Customer> GetByEmail(string email);
    }
}

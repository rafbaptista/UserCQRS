using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserCQRS.Domain.Entities;

namespace UserCQRS.Domain.Interfaces.Repositories
{
    public interface ICustomerRepository : IDisposable
    {
        Task Add(Customer customer);
        Task Update(Customer customer);
        Task Delete(Customer customer);
        Task<IEnumerable<Customer>> GetAll();
        Task<Customer> GetById(int id);        
    }
}

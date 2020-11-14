using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserCQRS.Domain.Entities;
using UserCQRS.Domain.Interfaces.Repositories;

namespace UserCQRS.Tests.FakeRepositories
{
    //Fake repository to test queries with dapper
    public class CustomerFakeRepository : ICustomerReadOnlyRepository
    {
        private readonly IList<Customer> _customers = new List<Customer>()
        {
            new Customer(1,"Rafael", "Baptista","rafabap100@gmail.com","Endereco",26),
            new Customer(2,"Rafael", "santos","rafasantos@gmail.com","Santos",30)
        };

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await Task.FromResult(_customers);
        }

        public async Task<Customer> GetByEmail(string email)
        {
            return await Task.FromResult(_customers.FirstOrDefault(x => x.Email == email));
        }     

        public async Task<Customer> GetById(int id)
        {
            return await Task.FromResult(_customers.FirstOrDefault(x => x.Id == id));            
        }
    }
}

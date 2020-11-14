using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserCQRS.Domain.Entities;
using UserCQRS.Domain.Interfaces.Repositories;
using UserCQRS.Infra.Data.Context;

namespace UserCQRS.Infra.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly UserCQRSContext _context;

        public CustomerRepository(UserCQRSContext context)
        {
            _context = context;
        }

        public async Task Add(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
        }

        public async Task Delete(Customer customer)
        {
            await Task.FromResult(_context.Customers.Remove(customer));            
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> GetById(int id)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);            
        }

        public async Task Update(Customer customer)
        {            
            await Task.FromResult(_context.Customers.Update(customer));
        }
    }
}

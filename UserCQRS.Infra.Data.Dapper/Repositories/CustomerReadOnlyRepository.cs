using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using UserCQRS.Domain.Entities;
using UserCQRS.Domain.Interfaces.Repositories;

namespace UserCQRS.Infra.Data.Dapper.Repositories
{
    public class CustomerReadOnlyRepository : ICustomerReadOnlyRepository
    {
        private readonly IConnectionFactory _factory;
        private IDbConnection _connection 
        {
            get
            {                
                return _factory.Connection();                             
            }
        }      

        public CustomerReadOnlyRepository(IConnectionFactory factory)
        {
            _factory = factory;           
        }

        public Task<IEnumerable<Customer>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Customer> GetByEmail(string email)
        {
            Customer customer = null;
            string sql = "SELECT * FROM Customers WHERE Email = @email";
            object queryParameters = new { Email = email };

            using (IDbConnection cn = _connection)
            {
                cn.Open();
                customer = await cn.QueryFirstOrDefaultAsync<Customer>(sql, queryParameters);
            }
            return customer;
        }

        public async Task<Customer> GetById(int id)
        {
            Customer customer = null;
            string sql = "SELECT * FROM Customers WHERE Id = @ID";
            object queryParameters = new { Id = id };

            using (IDbConnection cn = _connection)
            {
                cn.Open();
                customer = await cn.QueryFirstOrDefaultAsync<Customer>(sql, queryParameters);
            }
            return customer;
        }
    }
}

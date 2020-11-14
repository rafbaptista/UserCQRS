using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Threading.Tasks;
using UserCQRS.Domain.Interfaces.Repositories;
using UserCQRS.Domain.Queries.GetCustomerById;
using UserCQRS.Tests.FakeRepositories;

namespace UserCQRS.Tests.Domain.Queries.CustomerQueries
{
    [TestClass]
    public class GetCustomerByIdQueryTests : BaseTests
    {
        private IMapper _mapper;
        private ICustomerReadOnlyRepository _customerReadOnlyRepository;

        [TestInitialize]
        public void TestInitialize()
        {                                    
            _mapper = IMapper;
            _customerReadOnlyRepository = new CustomerFakeRepository();
        }                                               

        [TestMethod, TestCategory("Customer Queries by MSTest")]
        public async Task ShouldReturnOneCustomer()
        {                                                
            var command = new GetCustomerByIdQuery(1);
            var handler = new GetCustomerByIdQueryHandler(_mapper, _customerReadOnlyRepository);
            var result = await handler.Handle(command, CancellationToken.None);
            var customer = await _customerReadOnlyRepository.GetById(1);

            Assert.IsTrue(result != null);
            Assert.IsTrue(customer.Email == result.Email);
        }

        [TestMethod, TestCategory("Customer Queries by MSTest")]
        public async Task ShouldReturnNullIfCustomerDoesntExist()
        {
            var command = new GetCustomerByIdQuery(-1);
            var handler = new GetCustomerByIdQueryHandler(_mapper, _customerReadOnlyRepository);
            var result = await handler.Handle(command, CancellationToken.None);            

            Assert.IsTrue(result == null);            
        }

    }
}

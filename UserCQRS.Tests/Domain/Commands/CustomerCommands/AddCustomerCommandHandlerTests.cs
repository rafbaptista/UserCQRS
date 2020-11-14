using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserCQRS.Domain.Commands.CustomerCommands.AddCustomerCommand;
using UserCQRS.Domain.Entities;
using UserCQRS.Domain.Interfaces.Repositories;
using UserCQRS.Domain.Interfaces.Transactions;
using UserCQRS.Infra.Data.Context;
using UserCQRS.Infra.Data.Repositories;

namespace UserCQRS.Tests.Domain.Commands.CustomerCommands
{
    [TestClass]
    public class AddCustomerCommandHandlerTests : BaseTests
    {
        private UserCQRSContext _inMemoryContext;
        private ICustomerRepository _customerRepository;
        private IUnitOfWork _uow;
        private IMapper _mapper;

        private static IEnumerable<object[]> InvalidCustomers
        {
            get
            {
                return new[]
                {
                    //First name incorrect
                 new object[] { new Customer() {Id = 1, FirstName = "", LastName = "Baptista", Address = "Endereco", Age = 25, Email = "email@mail.com" } },
                 new object[] { new Customer() {Id = 1, FirstName = " ", LastName = "Baptista", Address = "Endereco", Age = 25, Email = "email@mail.com" } },
                 new object[] { new Customer() {Id = 1, FirstName = null, LastName = "Baptista", Address = "Endereco", Age = 25, Email = "email@mail.com" } },

                 //Last name incorrect
                 new object[] { new Customer() {Id = 1, FirstName = "Rafael", LastName = "", Address = "Endereco", Age = 25, Email = "email@mail.com" } },
                 new object[] { new Customer() {Id = 1, FirstName = "Rafael", LastName = " ", Address = "Endereco", Age = 25, Email = "email@mail.com" } },
                 new object[] { new Customer() {Id = 1, FirstName = "Rafael", LastName = null, Address = "Endereco", Age = 25, Email = "email@mail.com" } },

                 //Address incorrect
                 new object[] { new Customer() {Id = 1, FirstName = "Rafael", LastName = "Baptista", Address = "", Age = 25, Email = "email@mail.com" } },
                 new object[] { new Customer() {Id = 1, FirstName = "Rafael", LastName = "Baptista", Address = " ", Age = 25, Email = "email@mail.com" } },
                 new object[] { new Customer() {Id = 1, FirstName = "Rafael", LastName = "Baptista", Address = null, Age = 25, Email = "email@mail.com" } },

                 //Age incorrect
                 new object[] { new Customer() {Id = 1, FirstName = "Rafael", LastName = "Baptista", Address = "Endereco", Age = -1, Email = "email@mail.com" } },
                 new object[] { new Customer() {Id = 1, FirstName = "Rafael", LastName = "Baptista", Address = "Endereco", Age = 0, Email = "email@mail.com" } },

                 //Email incorrect
                 new object[] { new Customer() {Id = 1, FirstName = "Rafael", LastName = "Baptista", Address = "Endereco", Age = 25, Email = "" } },
                 new object[] { new Customer() {Id = 1, FirstName = "Rafael", LastName = "Baptista", Address = "Endereco", Age = 25, Email = " " } },
                 new object[] { new Customer() {Id = 1, FirstName = "Rafael", LastName = "Baptista", Address = "Endereco", Age = 25, Email = null } },                
                };   
            }
        }


        [TestInitialize]
        public void TestInitialize()
        {
            _inMemoryContext = Context;
            _customerRepository = new CustomerRepository(_inMemoryContext);
            _uow = GetUnitOfWork(_inMemoryContext);
            _mapper = IMapper;            
        }

        [TestCleanup]
        public void CleanUp()
        {
            _inMemoryContext.Dispose();
        }
        
        
        [TestMethod, TestCategory("Customer Commands by MSTest")]        
        public async Task ShouldInsertValidCustomer()
        {
            var command = new AddCustomerCommand("Rafael", "Baptista", "rafabap100@gmail.com", "Nenhum",26);
            var handler = new AddCustomerCommandHandler(_customerRepository, _uow, _mapper);

            var result = await handler.Handle(command, CancellationToken.None);
            var customer = _inMemoryContext.Customers.FirstOrDefault();

            Assert.AreEqual(_inMemoryContext.Customers.Count(), 1);
            Assert.IsTrue(result.Success == true);
            Assert.IsTrue(customer.Id != 0);
            Assert.AreEqual(command.FirstName, customer.FirstName);
            Assert.AreEqual(command.Age, customer.Age);                       
        }

        
        [DataTestMethod, TestCategory("Customer Commands by MSTest")]    
        [DynamicData(nameof(InvalidCustomers))]    //this unit test will run against each object
        public async Task ShouldNotInsertInvalidCustomer(Customer customer)
        {
            //Arrange
            var command = new AddCustomerCommand(customer.FirstName, customer.LastName, customer.Email, customer.Address, customer.Age);
            var handler = new AddCustomerCommandHandler(_customerRepository, _uow, _mapper);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsTrue(!result.Success);
            Assert.AreEqual(_inMemoryContext.Customers.Count(), 0);                                                       
        }       
    }
}

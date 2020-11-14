using AutoMapper;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserCQRS.Application.AutoMapper;
using UserCQRS.Domain.Commands.CustomerCommands.AddCustomerCommand;
using UserCQRS.Domain.Entities;
using UserCQRS.Domain.Interfaces.Repositories;
using UserCQRS.Domain.Interfaces.Transactions;
using Xunit;

namespace UserCQRS.xUnitTest.Domain.Commands.CustomerCommands
{
    public class AddCustomerCommandHandlerTests
    {
        public AddCustomerCommandHandlerTests()
        {
            _customerRepository = new Mock<ICustomerRepository>();
            _uow = new Mock<IUnitOfWork>();
        }

        private readonly Mock<ICustomerRepository> _customerRepository;
        private readonly Mock<IUnitOfWork> _uow;

        private IMapper _mapper
        {
            get
            {
                return new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new DomainToViewModelMappingProfile());
                    cfg.AddProfile(new ViewModelToDomainMappingProfile());
                }).CreateMapper();
            }
        }

        //Return invalid customers to be tested against unit tests
        public static IEnumerable<object[]> InvalidCustomers
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
                

        [Fact(DisplayName = nameof(ShouldInsertValidCustomer))]        
        [Trait("", "Customer Commands by xUnit && Moq")]
        public async Task ShouldInsertValidCustomer()
        {   
            //Arrange                         
            _customerRepository.Setup(cr => cr.Add(It.IsAny<Customer>())).Verifiable();
            var command = new AddCustomerCommand("Rafael", "Baptista", "rafabap100@gmail.com", "Nenhum", 26);            
            var handler = new AddCustomerCommandHandler(_customerRepository.Object, _uow.Object, _mapper);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.Success == true);
            _customerRepository.Verify(cr => cr.Add(It.IsAny<Customer>()), Times.Once);            
        }

        [Theory(DisplayName = nameof(ShouldNotInsertInvalidCustomer))]        
        [MemberData(nameof(InvalidCustomers))] //this unit test will run against each object                  
        [Trait("", "Customer Commands by xUnit && Moq")]
        public async Task ShouldNotInsertInvalidCustomer(Customer customer)
        {
            //Arrange
            _customerRepository.Setup(cr => cr.Add(It.IsAny<Customer>())).Verifiable();
            var command = new AddCustomerCommand(customer.FirstName, customer.LastName, customer.Email, customer.Address, customer.Age);
            var handler = new AddCustomerCommandHandler(_customerRepository.Object, _uow.Object, _mapper);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(!result.Success);
            _customerRepository.Verify(cr => cr.Add(It.IsAny<Customer>()), Times.Never);
        }

    }
}

using AutoMapper;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using UserCQRS.Application.AutoMapper;
using UserCQRS.Domain.Entities;
using UserCQRS.Domain.Interfaces.Repositories;
using UserCQRS.Domain.Queries.GetCustomerById;
using Xunit;

namespace UserCQRS.xUnitTest.Domain.Queries.CustomerQueries
{
    public class GetCustomerByIdQueryTests
    {
        private readonly Mock<ICustomerReadOnlyRepository> _customerReadOnlyRepository;

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

        public GetCustomerByIdQueryTests()
        {
            _customerReadOnlyRepository = new Mock<ICustomerReadOnlyRepository>();
        }

        [Fact(DisplayName = nameof(ShouldReturnOneCustomer))]        
        [Trait("", "Customer Queries by xUnit && Moq")]
        public async Task ShouldReturnOneCustomer()
        {
            //Arrange            
            Customer expected = new Customer() { Email = "email@mail.com", Age = 25 };

            _customerReadOnlyRepository.Setup(x => 
                                        x.GetById(It.IsAny<int>()))
                                        .Returns(Task.FromResult(expected));

            var command = new GetCustomerByIdQuery(1);                       
            var handler = new GetCustomerByIdQueryHandler(_mapper, _customerReadOnlyRepository.Object);

            //Act            
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("email@mail.com", result.Email);
        }

        [Fact(DisplayName = nameof(ShouldReturnNullIfCustomerDoesntExist))]
        [Trait("", "Customer Queries by xUnit && Moq")]
        public async Task ShouldReturnNullIfCustomerDoesntExist()
        {
            //Arrange
            Customer expected = null;

            _customerReadOnlyRepository.Setup(x =>
                                        x.GetById(It.IsAny<int>()))
                                        .Returns(Task.FromResult(expected))
                                        .Verifiable();

            //Act
            var command = new GetCustomerByIdQuery(1);
            var handler = new GetCustomerByIdQueryHandler(_mapper, _customerReadOnlyRepository.Object);

            //Assert
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.Null(result);
            _customerReadOnlyRepository.Verify(x => x.GetById(It.IsAny<int>()), Times.Once);
        }

    }
}

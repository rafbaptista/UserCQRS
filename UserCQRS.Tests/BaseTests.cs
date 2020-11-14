using AutoMapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using UserCQRS.Application.AutoMapper;
using UserCQRS.Domain.Interfaces.Transactions;
using UserCQRS.Infra.Data.Context;
using UserCQRS.Infra.Data.Transactions;
using UserCQRS.Tests.Factory;

namespace UserCQRS.Tests
{
    public abstract class BaseTests
    {
        protected IConfiguration Configuration
        {
            get
            {
                var builder = new ConfigurationBuilder();
                builder.AddInMemoryCollection(new Dictionary<string, string>()
                {
                    {"key", "value"}
                });
                return builder.Build();
            }
            set
            {
                Configuration = value;
            }
        }

        //Creates a in-memory database for tests purpose
        protected UserCQRSContext Context
        {
            get
            {
                return DatabaseFactory.Create();
            }
        }    
               
        //must share same in-memory context as the unit test
        public IUnitOfWork GetUnitOfWork(UserCQRSContext context)
        {
            return new UnitOfWork(context);
        }

        protected IMapper IMapper
        {
            get 
            {
                var config = new MapperConfiguration(config =>
                {
                    config.AddProfile<DomainToViewModelMappingProfile>();
                    config.AddProfile<ViewModelToDomainMappingProfile>();
                });
                return config.CreateMapper();
            }            
        }
    }
}

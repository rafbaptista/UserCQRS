using AutoMapper;
using UserCQRS.Domain.Commands.CustomerCommands.AddCustomerCommand;
using UserCQRS.Domain.Commands.CustomerCommands.DeleteCustomerCommand;
using UserCQRS.Domain.Commands.CustomerCommands.UpdateCustomerCommand;
using UserCQRS.Domain.Entities;
using UserCQRS.Domain.ViewModels;

namespace UserCQRS.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Customer, CustomerViewModel>();
            CreateMap<Customer, UpdateCustomerCommand>();
            CreateMap<Customer, DeleteCustomerCommand>();

            CreateMap<AddCustomerCommand, CustomerViewModel>();



        }
    }
}

using AutoMapper;
using UserCQRS.Domain.Commands.CustomerCommands.AddCustomerCommand;
using UserCQRS.Domain.Commands.CustomerCommands.UpdateCustomerCommand;
using UserCQRS.Domain.Entities;
using UserCQRS.Domain.Queries.GetCustomerById;
using UserCQRS.Domain.ViewModels;

namespace UserCQRS.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<CustomerViewModel, AddCustomerCommand>();
            CreateMap<CustomerViewModel, GetCustomerByIdQuery>();
            CreateMap<CustomerViewModel, UpdateCustomerCommand>();
                

            CreateMap<AddCustomerCommand, Customer>();

            
        }
    }
}
